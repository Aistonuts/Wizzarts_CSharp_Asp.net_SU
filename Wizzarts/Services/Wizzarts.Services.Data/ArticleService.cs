namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Article;

    using static Wizzarts.Common.AdminConstants;

    public class ArticleService : IArticleService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IMemoryCache cache;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IMemoryCache cache)
        {
            this.articleRepository = articleRepository;
            this.cache = cache;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var articles = this.GetCachedData<T>()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();
            return articles;
        }

        public async Task<int> GetCount()
        {
            return await this.articleRepository.All().CountAsync();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.GetCachedData<T>()
             .Take(count)
             .ToList();
        }

        public IEnumerable<T> GetCachedData<T>()
        {
            var cachedArticles = this.cache.Get<IEnumerable<T>>(ArticlesCacheKey);

            if (cachedArticles == null)
            {
                cachedArticles = this.articleRepository.AllAsNoTracking().OrderByDescending(x => x.Id).Where(x => x.ApprovedByAdmin == true && x.ForMainPage == true).To<T>().ToList();

                if (cachedArticles.Any())
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    this.cache.Set(ArticlesCacheKey, cachedArticles, cacheOptions);
                }
            }

            return cachedArticles;
        }

        public async Task CreateAsync(CreateArticleViewModel input, string userId, string imagePath, bool isPremium)
        {
            var article = new Article
            {
                Title = input.Title,
                Description = input.Description,
                ShortDescription = input.ShortDescription,
                ArticleCreatorId = userId,
                ForMainPage = isPremium,
            };
            Directory.CreateDirectory($"{imagePath}/navigation/articles");
            var extension = Path.GetExtension(input.ImageUrl.FileName)!.TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/navigation/articles/{article.Title.Replace(" ", string.Empty)}.{extension}";
            article.ImageUrl = $"/images/navigation/articles/{article.Title.Replace(" ", string.Empty)}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.ImageUrl.CopyToAsync(fileStream);
            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
            this.cache.Remove(ArticlesCacheKey);
        }

        public async Task UpdateAsync(int id, EditArticleViewModel input)
        {
            var articles = await this.articleRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (articles != null)
            {
                articles.Title = input.Title;
                articles.Description = input.Description;

                await this.articleRepository.SaveChangesAsync();
                this.cache.Remove(ArticlesCacheKey);
            }
        }

        public async Task<T> GetById<T>(int id)
        {
            var article = await this.articleRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return article;
        }

        public async Task<IEnumerable<T>> GetAllArticlesByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var article = await this.articleRepository.AllAsNoTracking()
               .Where(x => x.ArticleCreatorId == id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToListAsync();

            return article;
        }

        public async Task<string> ApproveArticle(int id)
        {
            var article = await this.articleRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (article != null && article.ApprovedByAdmin == false)
            {
                article.ApprovedByAdmin = true;
                await this.articleRepository.SaveChangesAsync();
                this.cache.Remove(ArticlesCacheKey);
                return article.ArticleCreatorId;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ArticleExist(int id)
        {
            return await this.articleRepository
                .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<bool> HasUserWithIdAsync(int articleId, string userId)
        {
            return await this.articleRepository.AllAsNoTracking()
                .AnyAsync(a => a.Id == articleId && a.ArticleCreatorId == userId);
        }

        public async Task DeleteAsync(int id)
        {
            var art = await this.articleRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (art != null)
            {
                this.articleRepository.Delete(art);
                await this.articleRepository.SaveChangesAsync();
                this.cache.Remove(ArticlesCacheKey);
            }
        }

        public async Task<bool> ArticleTitleExist(string title)
        {
            return await this.articleRepository
              .AllAsNoTracking().AnyAsync(a => a.Title == title);
        }

        public async Task<IEnumerable<T>> GetAllUserArticles<T>()
        {
            var articles = await this.articleRepository.AllAsNoTracking()
                .Where(x => x.ForMainPage == false)
                .To<T>().ToListAsync();
            return articles;
        }
    }
}

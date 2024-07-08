namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Article;

    using static Wizzarts.Common.GlobalConstants;

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

        public int GetCount()
        {
            return this.articleRepository.All().Count();
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
                cachedArticles = this.articleRepository.AllAsNoTracking().OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(ArticlesCacheKey, cachedArticles, cacheOptions);
            }

            return cachedArticles;
        }

        public async Task CreateAsync(CreateArticleViewModel input, string userId, string imagePath)
        {
            var article = new Article
            {
                Title = input.Title,
                Description = input.Description,
                ShortDescription = input.ShortDescription,
                ArticleCreatorId = userId,
            };
            Directory.CreateDirectory($"{imagePath}/navigation/");
            var extension = Path.GetExtension(input.ImageUrl.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/navigation/articles/{article.Id}.{extension}";
            article.ImageUrl = $"/images/navigation/articles/{article.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.ImageUrl.CopyToAsync(fileStream);
            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
            this.cache.Remove(ArticlesCacheKey);
        }

        public async Task UpdateAsync(int id, EditArticleViewModel input)
        {
            var articles = this.articleRepository.All().FirstOrDefault(x => x.Id == id);
            articles.Title = input.Title;
            articles.Description = input.Description;

            await this.articleRepository.SaveChangesAsync();
            this.cache.Remove(ArticlesCacheKey);
        }

        public T GetById<T>(int id)
        {
            var article = this.articleRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return article;
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var article = this.articleRepository.AllAsNoTracking()
               .Where(x => x.ArticleCreatorId == id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToList();

            return article;
        }
    }
}

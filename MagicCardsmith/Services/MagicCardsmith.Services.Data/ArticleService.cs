namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Article;

    public class ArticleService : IArticleService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Article> articleRepository;


        public ArticleService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task CreateAsync(CreateArticleInputModel input, string userId, string imagePath)
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
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var articles = this.articleRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return articles;
        }

        public T GetById<T>(int id)
        {
            var article = this.articleRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return article;
        }

        public int GetCount()
        {
            return this.articleRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.articleRepository.All()
                .OrderBy(x => x.Id)
                .Take(count)
                .To<T>().ToList();
        }

        public async Task UpdateAsync(int id, EditArticleInputModel input)
        {
            var articles = this.articleRepository.All().FirstOrDefault(x => x.Id == id);
            articles.Title = input.Title;
            articles.Description = input.Description;
            articles.ImageUrl = input.ImageUrl;

            await this.articleRepository.SaveChangesAsync();
        }
    }
}

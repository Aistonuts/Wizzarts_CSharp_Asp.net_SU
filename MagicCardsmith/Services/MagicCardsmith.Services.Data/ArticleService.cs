namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
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

        public async Task CreateAsync(CreateArticleInputModel input, string userId)
        {
            var article = new Article
            {
                Title = input.Title,
                Description = input.Description,
                ArticleCreatorId = userId,
                ImageUrl = input.ImageUrl,
            };

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

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.articleRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>().ToList();
        }
    }
}

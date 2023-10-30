namespace MagicCardsHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;
    using MagicCardsHub.Web.ViewModels.Article;

    public class ArticleService : IArticleService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<Art> artRepository;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<Art> artRepository)
        {
            this.articleRepository = articleRepository;
            this.artRepository = artRepository;

        }

        public async Task CreateAsync(CreateArticleInputModel input, string userId, string artId, string artPath)
        {
            var article = new Article
            {
                Title = input.Title,
                Description = input.Description,
                ArtId = artId,
                ArticleCreatorId = userId,
                RemoteImageUrl = input.RemoteImageUrl,
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

        public T GetArtById<T>(string id)
        {
            var art = this.artRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return art;
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

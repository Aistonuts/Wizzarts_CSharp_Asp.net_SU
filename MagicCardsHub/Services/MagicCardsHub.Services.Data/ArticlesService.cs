namespace MagicCardsHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;
    using MagicCardsHub.Web.ViewModels.Article;

    public class ArticlesService : IArticlesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articleRepository = articlesRepository;
        }

        public async Task CreateAsync(CreateArticleInputModel input, string userId, string imagePath)
        {
            var article = new Article()
            {
                Title = input.Title,
                Description = input.Description,
                ArticleCreatorId = userId,
            };

            Directory.CreateDirectory($"{imagePath}/images/");
            foreach (var image in input.Image)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                article.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/images/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
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

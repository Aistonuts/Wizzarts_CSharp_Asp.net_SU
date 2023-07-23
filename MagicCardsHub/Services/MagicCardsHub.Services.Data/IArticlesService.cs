namespace MagicCardsHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Article;

    public interface IArticlesService
    {
        Task CreateAsync(CreateArticleInputModel input, string userId, string imagePath);

        IEnumerable<T> GetRandom<T>(int count);
    }
}

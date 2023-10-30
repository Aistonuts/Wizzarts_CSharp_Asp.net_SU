namespace MagicCardsHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(CreateArticleInputModel input, string userId, string artId, string artPath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        T GetArtById<T>(string id);

        IEnumerable<T> GetRandom<T>(int count);
    }
}

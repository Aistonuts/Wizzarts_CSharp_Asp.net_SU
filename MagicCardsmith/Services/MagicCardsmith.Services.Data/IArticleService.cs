namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(CreateArticleInputModel input, string artistId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        IEnumerable<T> GetRandom<T>(int count);
    }
}

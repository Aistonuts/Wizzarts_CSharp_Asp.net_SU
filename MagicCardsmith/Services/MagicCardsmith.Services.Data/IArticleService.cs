namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(CreateArticleInputModel input, string artistId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditArticleInputModel input);

        T GetById<T>(int id);

        int GetCount();

        IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task ApproveArticle(int id);
    }
}

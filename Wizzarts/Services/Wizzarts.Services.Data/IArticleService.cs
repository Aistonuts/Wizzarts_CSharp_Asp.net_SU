namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(CreateArticleViewModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditArticleViewModel input);

        T GetById<T>(int id);

        int GetCount();

        IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3);
    }
}

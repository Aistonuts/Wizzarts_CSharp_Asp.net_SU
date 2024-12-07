namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(CreateArticleViewModel input, string userId, string imagePath, bool isPremium);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        Task<IEnumerable<T>> GetAllUserArticles<T>();

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditArticleViewModel input);

        Task DeleteAsync(int id);

        Task<T> GetById<T>(int id);

        Task<int> GetCount();

        Task<IEnumerable<T>> GetAllArticlesByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task<string> ApproveArticle(int id);

        Task<bool> ArticleExist(int id);

        Task<bool> HasUserWithIdAsync(int articleId, string userId);

        Task<bool> ArticleTitleExist(string title);
    }
}

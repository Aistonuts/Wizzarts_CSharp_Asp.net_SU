namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Premium;

    public interface IUserService
    {
        IEnumerable<T> GetAllAvatars<T>();

        int GetCountOfArt(string id);

        int GetCountOfEvents(string id);

        int GetCountOfArticles(string id);

        IEnumerable<T> GetAllArtByUserId<T>(string id);

        T GetAvatarById<T>(int id);

        IEnumerable<T> GetAllAsync<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);

        Task UpdateAsync(string id, CreateProfileViewModel input);
    }
}

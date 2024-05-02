namespace MagicCardsmith.Services.Data
{
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Premium;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        IEnumerable<T> GetAllAvatars<T>();

        int GetCountOfArt(string id);

        IEnumerable<T> GetAllArtByUserId<T>(string id);

        Task<bool> HasArtByUserIdAsync(string userId);

        bool IsStoreOwner(string userId);

        bool IsContentCreator(string userId);

        bool IsArtist(string userId);

        bool HasPublishedContent(string userId);

        bool HasEventParticipation(string userId);

        T GetAvatarById<T>(int id);

        IEnumerable<T> GetAllAsync<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);

        Task UpdateAsync(string id, CreateProfileViewModel input);
    }
}

namespace MagicCardsmith.Services.Data
{
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
    }
}

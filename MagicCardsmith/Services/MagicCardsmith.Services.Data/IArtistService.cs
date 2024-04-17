namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MagicCardsmith.Web.ViewModels.ElevatedRights;

    public interface IArtistService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        Task<bool> ArtistExistsByUserIdAsync(string userId);

        Task<string?> GetArtistIdByUserIdAsync(string userId);

        int GetCount();

        Task Create(string userId, BecomeArtistViewModel model);

        Task<bool> HasArtByUserIdAsync(string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);
    }
}

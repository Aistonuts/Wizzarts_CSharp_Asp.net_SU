namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Art;

    public interface IArtService
    {
        Task CreateAsync(CreateArtInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetAllByArtistId<T>(int id);

        IEnumerable<T> GetAllArtByUserId<T>(string id);

        IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3);

        IEnumerable<T> GetRandom<T>(int count);

        Task DeleteAsync(string id);

        bool IsBase64String(string base64);

        Task UpdateAsync(string id, BaseCreateArtInputModel input);

        Task ApproveArt(string id);
    }
}

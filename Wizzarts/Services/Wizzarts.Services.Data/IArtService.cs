namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Art;

    public interface IArtService
    {
        Task AddAsync(AddArtViewModel input, string userId, string imagePath, bool isPremium);

        Task UpdateAsync(EditArtViewModel input, string id);

        Task DeleteAsync(string id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetAllArtByUserId<T>(string id, int page, int itemsPerPage = 3);

        IEnumerable<T> GetAllArtByUserIdPaginationless<T>(string id);

        IEnumerable<T> GetRandom<T>(int count);

        Task<T> GetById<T>(string id);

        Task<int> GetCountAsync();

        bool IsBase64String(string base64);

        Task<string> ApproveArt(string id);

        Task<bool> ArtExist(string id);

        Task<bool> HasUserWithIdAsync(string artId, string userId);
    }
}

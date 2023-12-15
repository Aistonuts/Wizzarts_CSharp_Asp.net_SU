namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Art;

    public interface IArtService
    {
        Task CreateAsync(CreateArtInputModel input, int artistId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetAllByArtistId<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);
    }
}

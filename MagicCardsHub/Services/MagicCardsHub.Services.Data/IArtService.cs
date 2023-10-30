namespace MagicCardsHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Art;

    public interface IArtService
    {
        Task CreateAsync(CreateArtInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);
    }
}

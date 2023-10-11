namespace MagicCardsHub.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.DigitalArt;

    public interface IDigitalArtService
    {
        Task CreateAsync(CreateDigitalArtInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);
    }
}

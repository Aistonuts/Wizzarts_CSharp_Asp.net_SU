namespace MagicCardsHub.Services.Data
{
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.DigitalArt;

    public interface IDigitalArtService
    {
        Task CreateAsync(CreateDigitalArtInputModel input, string userId, string imagePath);
    }
}

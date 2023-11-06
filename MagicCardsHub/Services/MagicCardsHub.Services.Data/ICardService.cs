namespace MagicCardsHub.Services.Data
{
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Art;
    using MagicCardsHub.Web.ViewModels.Card;

    public interface ICardService
    {
        Task CreateAsync(CreateCardByArtIdInputModel input, string userId, string id, string imagePath);

        T GetArtById<T>(string id);

        T GetCardById<T>(string id);
    }
}

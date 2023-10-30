namespace MagicCardsHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Art;
    using MagicCardsHub.Web.ViewModels.CardSet;

    public interface ISetOfCardsService
    {
        Task CreateAsync(CreateCardSetInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);
    }
}

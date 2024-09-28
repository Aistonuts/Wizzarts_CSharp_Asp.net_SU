using System.Collections.Generic;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Deck;

namespace Wizzarts.Services.Data
{
    public interface IDeckService
    {
        Task CreateAsync(CreateDeckViewModel input, string userId, string imagePath);

        Task UpdateAsync(EditDeckViewModel input, int id);

        Task ChangeStatusAsync(SingleDeckViewModel input);

        Task<int> LockDeck(int id);

        IEnumerable<T> GetAllDecksByUserId<T>(string id);

        IEnumerable<T> GetAllCardsInDeckId<T>(int id);

        IEnumerable<DeckStatusListViewModel> GetAllDeckStatuses();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task<int> AddAsync(int deckId, string cardId);

        Task<int> RemoveAsync(int deckId, string cardId);

        bool HasEventCards(int id);
    }
}

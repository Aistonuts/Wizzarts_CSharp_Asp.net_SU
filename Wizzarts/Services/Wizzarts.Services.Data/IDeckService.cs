namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Deck;

    public interface IDeckService
    {
        Task CreateAsync(CreateDeckViewModel input, string userId, string imagePath);

        Task UpdateAsync(EditDeckViewModel input, int id);

        Task UpdateShippingAsync(SingleDeckViewModel input);

        Task ChangeStatusAsync(SingleDeckViewModel input);

        Task<int> LockDeck(int id);

        Task<IEnumerable<T>> GetAllDecksByUserId<T>(string id);

        Task<IEnumerable<T>> GetAllCardsInDeckId<T>(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<T> GetById<T>(int id);

        Task<int> AddAsync(int deckId, string cardId);

        Task<int> RemoveAsync(int deckId, string cardId);

        Task<bool> IsLocked(int id);

        Task<bool> HasOpenDecks(string id);

        Task OrderAsync(int deckId, string userId);

        Task DeleteAsync(int id);

        Task<bool> HasUserWithIdAsync(int deckId, string userId);
    }
}

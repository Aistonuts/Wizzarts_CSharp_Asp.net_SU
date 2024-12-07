namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.PlayCard;

    public interface IPlayCardService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetAllNoPagination<T>();

        Task<IEnumerable<T>> GetAllEventCards<T>();

        Task<T> GetById<T>(string id);

        Task DeleteAsync(string id);

        Task<int> GetCount();

        IEnumerable<T> GetRandom<T>(int count);

        Task CreateAsync(CreateCardViewModel input, string userId, int id, string path, bool isEventCard, bool requireArtInput, string canvasCapture);

        Task AddAsync(CreateCardViewModel input, string userId, string path, bool isEventCard, string canvasCapture);

        Task<IEnumerable<T>> GetAllCardManaByCardId<T>(string id);

        Task<IEnumerable<T>> GetAllCardsByExpansion<T>(int id);

        Task<IEnumerable<T>> GetAllCardsByUserId<T>(string id, int page, int itemsPerPage = 12);

        Task<string> ApproveCard(string id);

        Task<bool> CardExist(string id);

        Task<bool> HasUserWithIdAsync(string artId, string userId);

        Task<IEnumerable<T>> GetAllCardsByCriteria<T>(SingleDeckViewModel input);

        T GetByName<T>(string name);

        Task Promote(string id);

        Task<bool> CardTitleExist(string title);
    }
}

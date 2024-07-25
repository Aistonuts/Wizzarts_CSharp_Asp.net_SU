namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wizzarts.Web.ViewModels.PlayCard;

    public interface IPlayCardService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetRandom<T>(int count);

        Task CreateAsync(CreateCardViewModel input, string userId, int id, string path, bool isEventCard, bool requireArtInput, string canvasCapture);

        IEnumerable<T> GetAllCardManaByCardId<T>(string id);

        IEnumerable<T> GetAllCardsByUserId<T>(string id, int page, int itemsPerPage = 12);
    }
}

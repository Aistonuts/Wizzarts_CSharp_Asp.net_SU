using System.Collections.Generic;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.PlayCard;

namespace Wizzarts.Services.Data
{
    public interface IEventService
    {
        Task CreateAsync(CreateEventViewModel input, string userId, string imagePath, bool isContentCreator);

        Task UpdateAsync(EditEventViewModel input, int id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllEventComponents<T>(int id);

        T GetById<T>(int id);

        T GetEventComponentById<T>(int id);

        IEnumerable<T> GetAllEventsByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task<bool> EventExist(int id);

        Task<bool> HasUserWithIdAsync(int articleId, string userId);

        Task DeleteAsync(int id);

        Task<string> ApproveEvent(int id);

        Task AddComponentAsync(MyEventSettingsViewModel input, string userId, string imagePath);

        Task DeleteComponentAsync(int id);
    }
}

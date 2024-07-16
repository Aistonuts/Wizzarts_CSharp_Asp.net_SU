using System.Collections.Generic;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Event;

namespace Wizzarts.Services.Data
{
    public interface IEventService
    {
        Task CreateAsync(CreateEventViewModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllEventComponents<T>(int id);

        T GetById<T>(int id);

        T GetEventComponentById<T>(int id);

        IEnumerable<T> GetAllEventsByUserId<T>(string id, int page, int itemsPerPage = 3);
    }
}

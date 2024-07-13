using System.Collections.Generic;

namespace Wizzarts.Services.Data
{
    public interface IEventService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllEventComponents<T>(int id);

        T GetById<T>(int id);

        T GetEventComponentById<T>(int id);

        IEnumerable<T> GetAllEventsByUserId<T>(string id, int page, int itemsPerPage = 3);
    }
}

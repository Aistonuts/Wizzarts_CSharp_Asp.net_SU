using System.Collections.Generic;

namespace Wizzarts.Services.Data
{
    public interface IEventService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllEventComponents<T>(int id);
    }
}

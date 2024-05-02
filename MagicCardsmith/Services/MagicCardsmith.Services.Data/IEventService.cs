namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllMilestones<T>(int id);

        T GetById<T>(int id);

        T GetMilestoneById<T>(int Id);

        IEnumerable<T> GetAllEventCards<T>();

        IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task ApproveEvent(int id);
    }
}

namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Event;

    public interface IEventService
    {
        Task CreateAsync(CreateEventViewModel input, string eventCreator, string imagePath);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllStatuses<T>();

        IEnumerable<T> GetAllMilestones<T>(int id);

        T GetById<T>(int id);

        T GetMilestoneById<T>(int Id);

        IEnumerable<T> GetAllEventCards<T>();

        IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task ApproveEvent(int id);
    }
}

namespace Wizzarts.Web.ViewModels.Event
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventCategoryInListViewModel : IMapFrom<EventCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

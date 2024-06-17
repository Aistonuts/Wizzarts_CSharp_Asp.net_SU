namespace MagicCardsmith.Web.ViewModels.Event
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class EventStatusInListViewModel : IMapFrom<EventStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}

namespace MagicCardsmith.Web.ViewModels.Event
{
    using System.Collections.Generic;

    public class EventStatusListViewModel
    {
        public IEnumerable<EventStatusInListViewModel> EventStatuses { get; set; }
    }
}

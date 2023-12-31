namespace MagicCardsmith.Web.ViewModels.Event
{
    using System.Collections.Generic;

    public class EventListViewModel
    {
        public IEnumerable<EventInListViewModel> Events { get; set; }
    }
}

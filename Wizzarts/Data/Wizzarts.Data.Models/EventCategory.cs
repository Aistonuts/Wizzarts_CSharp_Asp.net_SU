namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Wizzarts.Data.Common.Models;

    public class EventCategory : BaseDeletableModel<int>
    {
        public EventCategory()
        {
            this.Events = new HashSet<Event>();
        }

        public string Title { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

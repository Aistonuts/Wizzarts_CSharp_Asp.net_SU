namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

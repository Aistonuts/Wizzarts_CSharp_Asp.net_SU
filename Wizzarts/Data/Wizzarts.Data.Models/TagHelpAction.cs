namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Wizzarts.Data.Common.Models;

    public class TagHelpAction : BaseDeletableModel<string>
    {
        public TagHelpAction()
        {
            this.Events = new HashSet<Event>();
            this.EventComponents = new HashSet<EventComponent>();
        }

        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<EventComponent> EventComponents { get; set; }
    }
}

namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.EventMilestones = new HashSet<EventMilestone>();
        }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string EventCreatorId { get; set; }

        public string RemoteImageUrl { get; set; }

        public int EventStatusId { get; set; }

        public EventStatus Status { get; set; }

        public ApplicationUser EventCreator { get; set; }

        public ICollection<EventMilestone> EventMilestones { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}

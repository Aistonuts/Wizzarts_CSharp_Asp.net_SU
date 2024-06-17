namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.EventData = new HashSet<EventData>();
        }

        [Required]
        [MaxLength(EventTitleMaxLength)]
        [Comment("Event Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        [Comment("Event Description")]
        public string EventDescription { get; set; } = string.Empty;

        [Required]
        [Comment("Event creator")]
        public string EventCreatorId { get; set; } = string.Empty;

        [Required]
        [Comment("Event image url")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Event status")]
        public int EventStatusId { get; set; }

        public EventStatus Status { get; set; }

        [Comment("Is event approved by admin.")]
        public bool ApprovedByAdmin { get; set; }

        [ForeignKey(nameof(EventCreatorId))]
        public WizzartsMember EventCreator { get; set; }

        public virtual ICollection<EventData> EventData { get; set; }
    }
}

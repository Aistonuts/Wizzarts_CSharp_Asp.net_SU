namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static MagicCardsmith.Common.DataConstants;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.EventMilestones = new HashSet<EventMilestone>();
            this.Cards = new HashSet<Card>();
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
        public ApplicationUser EventCreator { get; set; }

        public virtual ICollection<EventMilestone> EventMilestones { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}

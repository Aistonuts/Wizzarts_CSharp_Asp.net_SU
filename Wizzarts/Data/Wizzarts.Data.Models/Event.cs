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
            this.EventComponents = new HashSet<EventComponent>();
            this.Participants = new HashSet<ApplicationUser>();
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
        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Event image url")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Event status")]
        public int EventStatusId { get; set; }

        public EventStatus Status { get; set; }

        [Comment("Is event approved by admin.")]
        public bool ApprovedByAdmin { get; set; }

        public bool IsContentCreator { get; set; }

        [Required]
        [Comment("Event creator")]
        public string EventCreatorId { get; set; } = string.Empty;

        [ForeignKey(nameof(EventCreatorId))]
        public ApplicationUser EventCreator { get; set; }

        public virtual ICollection<EventComponent> EventComponents { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}

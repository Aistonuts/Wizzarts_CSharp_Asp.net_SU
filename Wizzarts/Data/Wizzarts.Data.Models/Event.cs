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

        [ForeignKey(nameof(EventStatusId))]
        public EventStatus Status { get; set; }

        [Comment("Action name")]
        public string ActionId { get; set; } = string.Empty;

        [ForeignKey(nameof(ActionId))]
        public TagHelpAction ActionName { get; set; }

        [Comment("Controller name")]
        public string ControllerId { get; set; } = string.Empty;

        [ForeignKey(nameof(ControllerId))]
        public TagHelpController ControllerName { get; set; }

        [Comment("Is event approved by admin.")]
        public bool ApprovedByAdmin { get; set; }

        public bool ForMainPage { get; set; }

        [Required]
        [Comment("Event creator")]
        public string EventCreatorId { get; set; } = string.Empty;

        [ForeignKey(nameof(EventCreatorId))]
        public ApplicationUser EventCreator { get; set; }

        [Required]
        [Comment("Event creator")]
        public int EventCategoryId { get; set; }

        [ForeignKey(nameof(EventCategoryId))]
        public EventCategory EventCategory { get; set; }

        public virtual ICollection<EventComponent> EventComponents { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}

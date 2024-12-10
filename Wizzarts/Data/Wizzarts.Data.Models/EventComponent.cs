namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class EventComponent : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(EventMilestoneTitleMaxLength)]
        [Comment("Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventMilestoneDescriptionMaxLength)]
        [Comment("Description")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(EventMilestoneDescriptionMaxLength)]
        [Comment("Instructions")]
        public string Instructions { get; set; } = string.Empty;

        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Image")]
        public string ImageUrl { get; set; } = string.Empty;

        public int EventId { get; set; }

        public Event Event { get; set; }

        [Comment("Component type according to event type")]
        public int EventCategoryId { get; set; }

        [Comment("Action name")]
        public string ActionId { get; set; } = string.Empty;

        [ForeignKey(nameof(ActionId))]
        public TagHelpAction ActionName { get; set; }

        [Comment("Controller name")]
        public string ControllerId { get; set; } = string.Empty;

        [ForeignKey(nameof(ControllerId))]
        public TagHelpController ControllerName { get; set; }
    }
}

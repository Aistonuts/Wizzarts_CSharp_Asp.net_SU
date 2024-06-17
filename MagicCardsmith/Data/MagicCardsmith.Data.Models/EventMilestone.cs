namespace MagicCardsmith.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static MagicCardsmith.Common.DataConstants;

    public class EventMilestone : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(EventMilestoneTitleMaxLength)]
        [Comment("Milestone title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventMilestoneDescriptionMaxLength)]
        [Comment("Milestone description")]
        public string Description { get; set; } = string.Empty;

        [Comment("Milestone instructions")]
        public string Instructions { get; set; } = string.Empty;

        [Comment("Image")]
        public string ImageUrl { get; set; } = string.Empty;

        public int EventId { get; set; }

        public Event Event { get; set; }

        [Comment("Is milestone completed")]
        public bool IsCompleted { get; set; }

        [Comment("Does it require art input")]
        public bool RequireArtInput { get; set; }
    }
}

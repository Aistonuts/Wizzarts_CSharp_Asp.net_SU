namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

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

        [Comment("Instructions")]
        public string Instructions { get; set; } = string.Empty;

        [Comment("Image")]
        public string ImageUrl { get; set; } = string.Empty;

        public int EventId { get; set; }

        public Event Event { get; set; }

        [Comment("Does it require art input")]
        public bool RequireArtInput { get; set; }
    }
}

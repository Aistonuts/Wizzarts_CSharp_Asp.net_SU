namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class CommentCard : BaseDeletableModel<int>
    {
        [Required]
        [Comment("Card title")]
        public string CardName { get; set; } = string.Empty;

        [Required]
        [Comment("Card Description")]
        public string CardFlavor { get; set; } = string.Empty;

        [Required]
        [MaxLength(CardReviewMaxLength)]
        [Comment("CardReview")]
        public string Review { get; set; } = string.Empty;

        [MaxLength(CardReviewSuggestionMaxLength)]
        [Comment("What can be done to resolve the issue.")]
        public string Suggestions { get; set; } = string.Empty;

        [Required]
        [Comment("Review of which card")]
        public string CardId { get; set; }

        public PlayCard Card { get; set; }

        [Required]
        public string PostedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(PostedByUserId))]
        [Comment("Posted by user")]
        public virtual ApplicationUser PostedByUser { get; set; }

        public bool IsAdminComment { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Wizzarts.Web.ViewModels.CardComments
{
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class CardCommentInListViewModel : IMapFrom<CommentCard>
    {
        public int Id { get; set; }

        [Comment("Card Title this  comment is about")]
        public string Title { get; set; } = string.Empty;

        [Comment("Card description this  comment is about")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CardReviewMaxLength, MinimumLength = CardReviewMinLength, ErrorMessage = LengthMessage)]
        public string Review { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CardReviewSuggestionMaxLength, MinimumLength = CardReviewSuggestionMinLength, ErrorMessage = LengthMessage)]
        public string Suggestions { get; set; } = string.Empty;

        [Comment("Card id this  comment is about")]
        public string CardId { get; set; }

        public string PostedByUserId { get; set; } = string.Empty;

        public string PostedByUser { get; set; } = string.Empty;

        public bool IsPostedByAdmin { get; set; }
    }
}

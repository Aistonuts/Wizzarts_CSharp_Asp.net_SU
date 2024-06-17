namespace MagicCardsmith.Web.ViewModels.CardReview
{
    using System.ComponentModel.DataAnnotations;

    using static MagicCardsmith.Common.DataConstants;

    public class CreateCardReviewViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Review is required!")]
        [StringLength(CardReviewMaxLength, MinimumLength = CardReviewMinLength, ErrorMessage = "Card review should be between 5 and 500 characters long")]
        public string Review { get; set; }

        public string Suggestions { get; set; }

        public int CardId { get; set; }
    }
}

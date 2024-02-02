namespace MagicCardsmith.Web.ViewModels.CardReview
{
    using System.Collections.Generic;

    public class CardReviewListViewModel
    {
        public IEnumerable<CardReviewInListViewModel> CardReviews { get; set; }
    }
}

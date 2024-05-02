namespace MagicCardsmith.Web.ViewModels.CardReview
{
    using System.Collections.Generic;

    public class CardReviewListViewModel : PagingViewModel
    {
        public IEnumerable<CardReviewInListViewModel> CardReviews { get; set; }
    }
}

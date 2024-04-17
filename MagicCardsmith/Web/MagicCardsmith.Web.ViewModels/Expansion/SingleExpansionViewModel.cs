namespace MagicCardsmith.Web.ViewModels.Expansion
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;

    public class SingleExpansionViewModel : IMapFrom<GameExpansion>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExpansionSymbolUrl { get; set; }

        public string CardsCount { get; set; }

        public string Review { get; set; }

        public string Suggestions { get; set; }

        public int CardId { get; set; }

        public string PostedByUserId { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<CardReviewInListViewModel> CardReviews { get; set; }
    }
}

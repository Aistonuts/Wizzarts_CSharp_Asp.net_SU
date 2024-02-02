namespace MagicCardsmith.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using MagicCardsmith.Web.ViewModels.Stores;

    public class IndexViewModel
    {
        public IEnumerable<IndexPageArticleViewModel> Articles { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<ArtistInListViewModel> Artists { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public IEnumerable<StoresInListViewModel> Stores { get; set; }

        public IEnumerable<ExpansionInListViewModel> GameExpansions { get; set; }

        //public IEnumerable<CardTypeIdViewModel> CardTypes { get; set; }

        //public IEnumerable<CardByTypeListViewModel> CardByType { get; set; }
    }
}

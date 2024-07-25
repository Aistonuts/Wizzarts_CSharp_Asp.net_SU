namespace MagicCardsmith.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Chat;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using MagicCardsmith.Web.ViewModels.Stores;

    public class IndexViewModel
    {
        public int ChatId { get; set; }

        public string ChatName { get; set; }

        public string ChatType { get; set; }

        public IEnumerable<Chat> Chats {get; set; }

        public IEnumerable<Message> Messages { get; set; }

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

namespace Wizzarts.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.CardComments;
    using Wizzarts.Web.ViewModels.Chat;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.GameRules;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;
    using Wizzarts.Web.ViewModels.Store;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class IndexAuthenticationViewModel : PagingViewModel
    {
        [Required]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int ChatId { get; set; }

        public string MembershipStatus { get; set; } = string.Empty;

        public string CurrentRole { get; set; } = string.Empty;

        public int CountOfArts { get; set; }

        public int CountOfArticles { get; set; }

        public int CountOfEvents { get; set; }

        public int CountOfCards { get; set; }

        public bool IsProfileUpToDate { get; set; } = true;

        public bool HasOpenDeck { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public IEnumerable<DeckStatusListViewModel> DeckStatuses { get; set; }

        public IEnumerable<DeckInListViewModel> Decks { get; set; }

        public IEnumerable<EventComponentsInListViewModel> EventComponents { get; set; }

        public IEnumerable<ArticleInListViewModel> Articles { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public IEnumerable<StoreInListViewModel> Stores { get; set; }

        public IEnumerable<ExpansionInListViewModel> GameExpansions { get; set; }

        public IEnumerable<DbChatMessagesInListViewModel> ChatMessages { get; set; }

        public IEnumerable<SingleChatViewModel> ChatRooms { get; set; }

        public IEnumerable<WizzartsTeamInListViewModel> WizzartsTeamMembers { get; set; }

        public IEnumerable<GameRulesDataViewModel> GameRulesData { get; set; }

        public IEnumerable<CardCommentInListViewModel> Comments { get; set; }

        public IEnumerable<ManaListViewModel> Mana { get; set; }
    }
}

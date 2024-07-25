namespace Wizzarts.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Chat;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class IndexAuthenticationViewModel : PagingViewModel
    {
        [Required]
        public string UserName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int ChatId { get; set; }

        public IEnumerable<ArticleInListViewModel> Articles { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<WizzartsTeamInListViewModel> Artists { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public IEnumerable<StoreInListViewModel> Stores { get; set; }

        public IEnumerable<ExpansionInListViewModel> GameExpansions { get; set; }

        public IEnumerable<DbChatMessagesInListViewModel> ChatMessages { get; set; }
    }
}

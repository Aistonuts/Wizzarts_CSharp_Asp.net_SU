namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.MembershipConstants;

    public class MembershipViewModel : IndexAuthenticationViewModel
    {
        public int ArtistRoleNeededArts { get; set; } = MemberToArtistRequiredArts;

        public int AllRolesRequiredArticles { get; set; } = RequiredNumberArticles;

        public int AllRolesCards { get; set; } = RequiredNumberEventCards;

        public int AllRolesEvents { get; set; } = RequiredNumberEvents;

        public int ArtNeeded { get; set; }

        public int ArticlesNeeded { get; set; }

        public int CardsNeeded { get; set; }

        public int EventsNeeded { get; set; }
    }
}

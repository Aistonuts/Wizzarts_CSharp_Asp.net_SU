using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class MembershipViewModel
    {
        public const int ArtistRequiredArts = 3;

        public const int PremiumRequiredArts = 10;

        public const int RequiredNumberArticles = 1;

        public const int RequiredNumberCards = 1;

        public const int RequiredNumberEvents = 1;

        public string CurrentRole { get; set; } = string.Empty;

        public bool IsMember { get; set; }

        public bool IsStoreOwner { get; set; }

        public bool IsArtist { get; set; }

        public bool IsPremiumUser { get; set; }

        public int ArtistRoleNeededArts { get; set; } = ArtistRequiredArts;

        public int PremiumRoleNeededArts { get; set; } = PremiumRequiredArts;

        public int AllRolesRequiredArticles { get; set; } = RequiredNumberArticles;

        public int AllRolesCards { get; set; } = RequiredNumberCards;

        public int AllRolesEvents { get; set; } = RequiredNumberEvents;

        public int ArtNeeded { get; set; }

        public int ArticlesNeeded { get; set; }

        public int CardsNeeded { get; set; }

        public int EventsNeeded { get; set; }
    }
}

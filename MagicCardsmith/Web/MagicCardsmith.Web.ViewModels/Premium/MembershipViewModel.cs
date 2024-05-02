namespace MagicCardsmith.Web.ViewModels.Premium
{
    using System.Collections.Generic;

    public class MembershipViewModel
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public bool HasApprovedArt { get; set; }

        public bool HasApprovedContent { get; set; }

        public bool EventParticipation { get; set; }

        public bool IsAnArtist { get; set; }

        public bool IsPremiumUser { get; set; }
    }
}

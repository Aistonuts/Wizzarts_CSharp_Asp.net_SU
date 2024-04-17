namespace MagicCardsmith.Web.ViewModels.ElevatedRights
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ClaimViewModel
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public string NumberOfArtPieces { get; set; }

        public bool EventParticipation { get; set; }

        public bool IsStoreOwner { get; set; }

        public bool IsAnArtist { get; set; }

        public bool HasContentApprovedByAdmin { get; set; }

        public IEnumerable<AvatarListViewModel> AvatarListViewModels { get; set; }
    }
}

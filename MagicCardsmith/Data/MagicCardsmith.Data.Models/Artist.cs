namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Artist : BaseDeletableModel<int>
    {
        public Artist()
        {
            this.ArtPieces = new HashSet<Art>();
        }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Art> ArtPieces { get; set; }
    }
}

namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static MagicCardsmith.Common.DataConstants;

    public class Artist : BaseDeletableModel<int>
    {
        public Artist()
        {
            this.ArtPieces = new HashSet<Art>();
        }

        [Required]
        [MaxLength(UserNickNameMaxLength)]
        [PersonalData]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [Comment("Avatar remote URL.Picked after signing in")]
        public string AvatarUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Information about the artist")]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [Comment("Artist's eamil")]
        public string Email { get; set; } = string.Empty;

        [Comment("Artist's user id")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }

        [Comment("Is user artist applicaiton approved by the admin.")]
        public bool ApprovedByAdmin { get; set; }

        [Comment("Team MagicCardsmith art pieces")]
        public virtual ICollection<Art> ArtPieces { get; set; }
    }
}

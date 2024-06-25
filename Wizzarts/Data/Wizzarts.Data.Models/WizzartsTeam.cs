namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class WizzartsTeam : BaseDeletableModel<int>
    {

        [MaxLength(UserNickNameMaxLength)]
        [PersonalData]
        public string Nickname { get; set; } = string.Empty;

        [Comment("Information about the artist")]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [Comment("Avatar remote URL.Picked after signing in")]
        public string AvatarUrl { get; set; } = string.Empty;

        public string WizzartsCardGameId { get; set; }

        [ForeignKey(nameof(WizzartsCardGameId))]
        public WizzartsCardGame WizzartsCardGame { get; set; }

        [Comment("Wizzarts Team user id")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }
    }
}

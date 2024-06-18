namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class WizzartsTeam : BaseDeletableModel<string>
    {

        [MaxLength(UserNickNameMaxLength)]
        [PersonalData]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [Comment("Avatar remote URL.Picked after signing in")]
        public string AvatarUrl { get; set; } = string.Empty;

        [Comment("Game Rules published by Admin")]
        public string GameRulesId { get; set; } = string.Empty;

        public WizzartsGameRules GameRules { get; set; }

        [Comment("Wizzarts Team user id")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }
    }
}

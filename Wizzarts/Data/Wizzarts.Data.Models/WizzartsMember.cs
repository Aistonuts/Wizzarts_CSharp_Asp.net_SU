namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;
    using static Wizzarts.Common.DataConstants;

    public class WizzartsMember : BaseDeletableModel<string>
    {
        public WizzartsMember()
        {
            this.Articles = new HashSet<Article>();
            this.Cards = new HashSet<PlayCard>();
            this.Events = new HashSet<Event>();
            this.Stores = new HashSet<Store>();
            this.Votes = new HashSet<Vote>();
            this.Art = new HashSet<Art>();
            this.Comments = new HashSet<CommentCard>();
        }

        [Required]
        [MaxLength(UserNickNameMaxLength)]
        [PersonalData]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [Comment("Avatar remote URL.Picked after signing in")]
        public string AvatarUrl { get; set; } = string.Empty;

        [Comment("Information about the artist")]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [Comment("Artist's eamil")]
        public string Email { get; set; } = string.Empty;

        [Comment("Artist's user id")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }

        [Comment("Team MagicCardsmith art pieces")]
        public virtual ICollection<Art> ArtPieces { get; set; }


        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<PlayCard> Cards { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Art> Art { get; set; }

        public virtual ICollection<CommentCard> Comments { get; set; }
    }
}

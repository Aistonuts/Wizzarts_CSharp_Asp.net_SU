// ReSharper disable VirtualMemberCallInConstructor
namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Articles = new HashSet<Article>();
            this.Cards = new HashSet<PlayCard>();
            this.Events = new HashSet<Event>();
            this.Stores = new HashSet<Store>();
            this.Votes = new HashSet<Vote>();
            this.Art = new HashSet<Art>();
            this.Comments = new HashSet<CommentCard>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

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

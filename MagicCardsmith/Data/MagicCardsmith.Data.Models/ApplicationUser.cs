// ReSharper disable VirtualMemberCallInConstructor
namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;
    using MagicCardsmith.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Articles = new HashSet<Article>();
            this.Cards = new HashSet<Card>();
            this.Events = new HashSet<Event>();
            this.Stores = new HashSet<Store>();
            this.Votes = new HashSet<Vote>();
        }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
   }
}

// ReSharper disable VirtualMemberCallInConstructor
namespace MagicCardsHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.GameFormatProjects = new List<GameFormatProject>();
            this.Art = new HashSet<DigitalArt>();
            this.Stores = new HashSet<Store>();
            this.Tournaments = new HashSet<Tournament>();
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
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

        public virtual ICollection<GameFormatProject> GameFormatProjects { get; set; }

        public virtual ICollection<DigitalArt> Art { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}

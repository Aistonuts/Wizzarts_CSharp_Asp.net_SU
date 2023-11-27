namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        public Artist()
        {
            this.Id = Guid.NewGuid();
            this.Art = new HashSet<Art>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public Guid? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; } = null!;

        public virtual ICollection<Art> Art { get; set; }
    }
}

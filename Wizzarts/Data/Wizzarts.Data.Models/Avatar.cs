namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class Avatar : BaseDeletableModel<int>
    {
        public Avatar()
        {
            this.Members = new HashSet<ApplicationUser>();
        }

        [Comment("Avatar Name. Seeded.")]
        [MaxLength(ArtTitleMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Comment("Avatar Remote URL. Seeded.")]
        [MaxLength(RemoteImageUrlMaxLength)]
        public string AvatarUrl { get; set; } = string.Empty;

        public ICollection<ApplicationUser> Members { get; set; }
    }
}

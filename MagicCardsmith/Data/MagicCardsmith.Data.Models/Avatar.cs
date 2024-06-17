namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class Avatar : BaseDeletableModel<int>
    {
        public Avatar()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        [Comment("Avatar Name. Seeded.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Avatar Remote URL. Seeded.")]
        public string AvatarUrl { get; set; } = string.Empty;

        public ICollection<ApplicationUser> Users { get; set; }
    }
}

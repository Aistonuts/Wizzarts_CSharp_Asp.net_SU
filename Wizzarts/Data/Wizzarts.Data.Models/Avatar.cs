namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class Avatar : BaseDeletableModel<int>
    {
        public Avatar()
        {
            this.Members = new HashSet<ApplicationUser>();
        }

        [Comment("Avatar Name. Seeded.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Avatar Remote URL. Seeded.")]
        public string AvatarUrl { get; set; } = string.Empty;

        public ICollection<ApplicationUser> Members { get; set; }
    }
}

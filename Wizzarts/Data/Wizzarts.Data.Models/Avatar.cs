namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    public class Avatar
    {
        public Avatar()
        {
            this.Members = new HashSet<WizzartsMember>();
        }

        [Comment("Avatar Name. Seeded.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Avatar Remote URL. Seeded.")]
        public string AvatarUrl { get; set; } = string.Empty;

        public ICollection<WizzartsMember> Members { get; set; }
    }
}

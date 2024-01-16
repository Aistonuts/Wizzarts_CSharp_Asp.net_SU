namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Avatar : BaseDeletableModel<int>
    {
        public Avatar()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}

namespace MagicCardsHub.Data.Models
{
    using System.Collections.Generic;
    using MagicCardsHub.Data.Common.Models;

    public class Store : BaseModel<int>
    {
        public Store()
        {
            this.Tournaments = new HashSet<StoreTournament>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string StoreOwnerId { get; set; }

        public virtual ApplicationUser StoreOwner { get; set; }

        public virtual ICollection<StoreTournament> Tournaments { get; set; }
    }
}

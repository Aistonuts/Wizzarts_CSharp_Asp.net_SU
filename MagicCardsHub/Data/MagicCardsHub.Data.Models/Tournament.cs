namespace MagicCardsHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class Tournament : BaseModel<int>
    {
        public Tournament()
        {
            this.Stores = new HashSet<StoreTournament>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public string TournamentHostId { get; set; }

        public ApplicationUser TournamentHost { get; set; }

        public virtual ICollection<StoreTournament> Stores { get; set; }
    }
}

namespace MagicCardsHub.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class StoreTournament : BaseModel<int>
    {
        public StoreTournament()
        {
            this.Images = new HashSet<Image>();
        }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

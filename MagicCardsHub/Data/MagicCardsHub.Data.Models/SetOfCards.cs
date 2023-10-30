namespace MagicCardsHub.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class SetOfCards : BaseDeletableModel<int>
    {
        public SetOfCards()
        {
            this.Cards = new HashSet<Card>();
        }

        public string Expansion { get; set; }

        public string Description { get; set; }

        public int NumberOfCards { get; set; }

        public string ImageUrl { get; set; }

        public string CardSmithId { get; set; }

        public virtual ApplicationUser CardSmith { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}

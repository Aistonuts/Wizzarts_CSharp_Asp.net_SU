namespace MagicCardsHub.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class GameExpansion : BaseDeletableModel<int>
    {
        public GameExpansion()
        {
            this.Cards = new HashSet<Card>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }

        public string CardsCount { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}

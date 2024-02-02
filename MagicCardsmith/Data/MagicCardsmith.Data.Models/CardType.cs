namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class CardType : BaseDeletableModel<int>
    {
        public CardType()
        {
            this.Cards = new HashSet<Card>();
        }

        public string Name { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}

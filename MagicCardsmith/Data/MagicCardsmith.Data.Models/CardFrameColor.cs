namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class CardFrameColor : BaseDeletableModel<int>
    {
        public CardFrameColor()
        {
            this.Cards = new HashSet<Card>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}

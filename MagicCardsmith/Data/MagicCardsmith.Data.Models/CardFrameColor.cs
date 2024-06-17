namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class CardFrameColor : BaseDeletableModel<int>
    {
        public CardFrameColor()
        {
            this.Cards = new HashSet<Card>();
        }

        [Comment("Play Card Frame color. Seeded")]
        public string Name { get; set; } = string.Empty;

        [Comment("Play Remote Image. Seeded")]
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Card> Cards { get; set; }
    }
}

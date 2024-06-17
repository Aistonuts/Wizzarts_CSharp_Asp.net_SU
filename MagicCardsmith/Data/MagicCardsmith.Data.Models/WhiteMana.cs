namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class WhiteMana : BaseDeletableModel<int>
    {
        public WhiteMana()
        {
            this.Cards = new HashSet<Card>();
        }

        [Comment("Play Card Total Cost")]
        public int Cost { get; set; }

        [Comment("Play Card Mana Conor Name")]
        public string ColorName { get; set; } = string.Empty;

        [Comment("Play Card Remote URL. Seeded.")]
        public string ImageUrl { get; set; } = string.Empty;

        public virtual ICollection<Card> Cards { get; set; }
    }
}

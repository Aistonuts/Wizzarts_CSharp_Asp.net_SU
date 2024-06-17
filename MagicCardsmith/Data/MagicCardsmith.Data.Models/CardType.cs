namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class CardType : BaseDeletableModel<int>
    {
        public CardType()
        {
            this.Cards = new HashSet<Card>();
        }

        [Comment("Card type is.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Collect of cards by type.")]
        public virtual ICollection<Card> Cards { get; set; }
    }
}

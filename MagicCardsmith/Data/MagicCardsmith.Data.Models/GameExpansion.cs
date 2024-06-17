namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class GameExpansion : BaseDeletableModel<int>
    {
        public GameExpansion()
        {
            this.Cards = new HashSet<Card>();
        }

        [Comment("Card game expansion title.Seeded")]
        public string Title { get; set; } = string.Empty;

        [Comment("Card game expansion description. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Comment("Card game expansion symbol. Seeded")]
        public string ExpansionSymbolUrl { get; set; } = string.Empty;

        [Comment("Numbwer of cards by expansion. Seeded")]
        public string CardsCount { get; set; } = string.Empty;

        public virtual ICollection<Card> Cards { get; set; }
    }
}

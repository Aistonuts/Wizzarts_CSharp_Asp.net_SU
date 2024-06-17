namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class CadGameExpansion : BaseDeletableModel<int>
    {
        public CadGameExpansion()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        [Comment("Card game expansion title.Seeded")]
        public string Title { get; set; } = string.Empty;

        [Comment("Card game expansion description. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Comment("Card game expansion symbol. Seeded")]
        public string ExpansionSymbolUrl { get; set; } = string.Empty;

        [Comment("Numbwer of cards by expansion. Seeded")]
        public string CardsCount { get; set; } = string.Empty;

        public virtual ICollection<PlayCard> Cards { get; set; }
    }
}

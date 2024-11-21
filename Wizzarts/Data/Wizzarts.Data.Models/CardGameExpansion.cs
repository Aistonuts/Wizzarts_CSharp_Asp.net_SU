namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class CardGameExpansion : BaseDeletableModel<int>
    {
        public CardGameExpansion()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        [Required]
        [MaxLength(ExpansionTitleMaxLength)]
        [Comment("Card game expansion title.Seeded")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ExpansionDescriptionMaxLength)]
        [Comment("Card game expansion description. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Card game expansion symbol. Seeded")]
        public string ExpansionSymbolUrl { get; set; } = string.Empty;

        [Comment("Number of cards by expansion. Seeded")]
        public string CardsCount { get; set; } = string.Empty;

        public virtual ICollection<PlayCard> Cards { get; set; }
    }
}

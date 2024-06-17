namespace MagicCardsmith.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class CardMana : BaseDeletableModel<int>
    {
        [Comment("Mana color type.")]
        public string Color { get; set; } = string.Empty;

        [Comment("Mana remote image url.")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        public int CardId { get; set; }

        [ForeignKey(nameof(CardId))]
        public Card Card { get; set; }
    }
}

namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class ManaInCard : BaseDeletableModel<int>
    {
        [Comment("Mana color type.")]
        public string Color { get; set; } = string.Empty;

        [Comment("Mana remote image url.")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        public string CardId { get; set; }

        [ForeignKey(nameof(CardId))]
        public PlayCard Card { get; set; }
    }
}

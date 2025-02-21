namespace Wizzarts.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class ManaCost : BaseDeletableModel<int>
    {
        [Comment("Mana color type.")]
        public string Color { get; set; } = string.Empty;

        [Comment("Mana remote image url.")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Comment("Play Card Total Cost")]
        public int Cost { get; set; }
    }
}

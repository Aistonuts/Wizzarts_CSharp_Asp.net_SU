namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class PlayCardFrameColor : BaseDeletableModel<int>
    {
        public PlayCardFrameColor()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        [Comment("Play Card Frame color. Seeded")]
        public string Name { get; set; } = string.Empty;

        [Comment("Play Remote Image. Seeded")]
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<PlayCard> Cards { get; set; }
    }
}

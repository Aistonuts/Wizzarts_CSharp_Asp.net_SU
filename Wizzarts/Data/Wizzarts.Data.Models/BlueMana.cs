namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class BlueMana : BaseDeletableModel<int>
    {
        public BlueMana()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        [Comment("Play Card Total Cost")]
        public int Cost { get; set; }

        [Comment("Play Card Mana Color Name")]
        public string ColorName { get; set; } = string.Empty;

        [Comment("Play Card Remote URL. Seeded.")]
        public string ImageUrl { get; set; } = string.Empty;

        public virtual ICollection<PlayCard> Cards { get; set; }
    }
}

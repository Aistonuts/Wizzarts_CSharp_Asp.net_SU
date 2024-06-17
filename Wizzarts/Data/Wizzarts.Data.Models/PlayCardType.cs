namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class PlayCardType : BaseDeletableModel<int>
    {
        public PlayCardType()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        [Comment("Card type is.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Collect of cards by type.")]
        public virtual ICollection<PlayCard> Cards { get; set; }
    }
}

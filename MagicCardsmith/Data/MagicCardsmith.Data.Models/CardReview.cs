namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class CardReview : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Review { get; set; }

        public string Suggestions { get; set; }

        public int? CardId { get; set; }

        public Card Card { get; set; }

        public string PostedByUserId { get; set; }

        public virtual ApplicationUser PostedByUser { get; set; }
    }
}

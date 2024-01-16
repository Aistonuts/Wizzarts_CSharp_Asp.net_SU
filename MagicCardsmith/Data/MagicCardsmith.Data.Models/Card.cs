namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Card : BaseDeletableModel<int>
    {
        public Card()
        {
            this.CardMana = new HashSet<CardMana>();
        }

        public string Name { get; set; }

        public string CardFrameColor { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public int GameExpansionId { get; set; }

        public GameExpansion GameExpansion { get; set; }

        public bool IsEventCard { get; set; }

        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public string CardSmithId { get; set; }

        public virtual ApplicationUser CardSmith { get; set; }

        public virtual ICollection<CardMana> CardMana { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int CardReviewId { get; set; }

        public CardReview CardReview { get; set; }
    }
}

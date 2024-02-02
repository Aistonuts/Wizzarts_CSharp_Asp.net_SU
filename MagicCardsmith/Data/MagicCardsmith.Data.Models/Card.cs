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
            this.CardReviews = new HashSet<CardReview>();
        }

        public string Name { get; set; }

        public int? BlackManaId { get; set; }

        public BlackMana BlackMana { get; set; }

        public int? BlueManaId { get; set; }

        public BlueMana BlueMana { get; set; }

        public int? RedManaId { get; set; }

        public RedMana RedMana { get; set; }

        public int? WhiteManaId { get; set; }

        public WhiteMana WhiteMana { get; set; }

        public int? GreenManaId { get; set; }

        public GreenMana GreenMana { get; set; }

        public int? ColorlessManaId { get; set; }

        public ColorlessMana ColorlessMana { get; set; }

        public int? CardFrameColorId { get; set; }

        public CardFrameColor CardFrameColor { get; set; }

        public string CardRemoteUrl { get; set; }

        public int? CardTypeId { get; set; }

        public CardType CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public int? GameExpansionId { get; set; }

        public GameExpansion GameExpansion { get; set; }

        public bool IsEventCard { get; set; }

        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public string CardSmithId { get; set; }

        public virtual ApplicationUser CardSmith { get; set; }

        public virtual ICollection<CardMana> CardMana { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; set; }

        public virtual ICollection<CardReview> CardReviews { get; set; }
    }
}

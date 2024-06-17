namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static MagicCardsmith.Common.DataConstants;

    public class Card : BaseDeletableModel<int>
    {
        public Card()
        {
            this.CardMana = new HashSet<CardMana>();
            this.CardReviews = new HashSet<CardReview>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(CardNameMaxLength)]
        [Comment("Playcard name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Mana cost Id")]
        public int? BlackManaId { get; set; }

        public BlackMana BlackMana { get; set; }

        [Comment("Mana cost Id")]
        public int? BlueManaId { get; set; }

        public BlueMana BlueMana { get; set; }

        [Comment("Mana cost Id")]
        public int? RedManaId { get; set; }

        public RedMana RedMana { get; set; }

        [Comment("Mana cost Id")]
        public int? WhiteManaId { get; set; }

        public WhiteMana WhiteMana { get; set; }

        [Comment("Mana cost Id")]
        public int? GreenManaId { get; set; }

        public GreenMana GreenMana { get; set; }

        [Comment("Mana cost Id")]
        public int? ColorlessManaId { get; set; }

        public ColorlessMana ColorlessMana { get; set; }

        [Comment("Framecolor Id. There is a default value.")]
        public int? CardFrameColorId { get; set; }

        public CardFrameColor CardFrameColor { get; set; }

        [Comment("Image of the card saved locally upon creation.")]
        public string CardRemoteUrl { get; set; }

        [Comment("Card type identifier.")]
        public int? CardTypeId { get; set; }

        public CardType CardType { get; set; }

        [Required]
        [MaxLength(CardAbilitiesAndFlavorMaxLenght)]
        [Comment("Card use requirements and effects. Card power description.")]
        public string AbilitiesAndFlavor { get; set; } = string.Empty;

        [Comment("Card will deal damage equal to power.")]
        [MaxLength(CardPowerMaxLenght)]
        public string Power { get; set; } = string.Empty;

        [Comment("Card can take damage up to amount equal to its toughness.")]
        [MaxLength(CardToughnessMaxLenght)]
        public string Toughness { get; set; } = string.Empty;

        [Comment("This card is part of which expansion.")]
        public int? GameExpansionId { get; set; }

        public GameExpansion GameExpansion { get; set; }

        [Comment("Has this card been created during an event.")]
        public bool IsEventCard { get; set; }

        [Comment("Has this card been created during an event.")]
        public bool IsBanned { get; set; }

#nullable enable
        public string? ArtId { get; set; } = string.Empty;

        [Comment("Art from the database that has been used for creating this card.")]
        public virtual Art Art { get; set; }

        public string CardSmithId { get; set; } = string.Empty;

        [Comment("Who is the creator of this card")]
        [ForeignKey(nameof(CardSmithId))]
        public virtual ApplicationUser CardSmith { get; set; }

        [Comment("Has this card been approved by admin.")]
        public bool ApprovedByAdmin { get; set; }

        public int? EventId { get; set; }

        [Comment("Has this been created during an event.")]
        public Event Event { get; set; }

        public virtual ICollection<CardMana> CardMana { get; set; }

        public virtual ICollection<CardReview> CardReviews { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}

﻿namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class PlayCard : BaseDeletableModel<string>
    {
        public PlayCard()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CardMana = new HashSet<ManaInCard>();
            this.Comments = new HashSet<CommentCard>();
            this.Votes = new HashSet<Vote>();
            this.DeckOfCards = new HashSet<CardDeck>();
            this.CardsInOrder = new List<Order>();
        }

        [Required]
        [MaxLength(CardNameMaxLength)]
        [Comment("PlayCard name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Frame color Id. There is a default value.")]
        public int? CardFrameColorId { get; set; }

        public PlayCardFrameColor CardFrameColor { get; set; }

        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Image of the card saved locally upon creation.")]
        public string CardRemoteUrl { get; set; }

        [Comment("Card type identifier.")]
        public int? CardTypeId { get; set; }

        public PlayCardType CardType { get; set; }

        [Required]
        [MaxLength(CardAbilitiesAndFlavorMaxLength)]
        [Comment("Card use requirements and effects. Card power description.")]
        public string AbilitiesAndFlavor { get; set; } = string.Empty;

        [Comment("Card will deal damage equal to power.")]
        [MaxLength(CardPowerMaxLength)]
        public string Power { get; set; } = string.Empty;

        [Comment("Card can take damage up to amount equal to its toughness.")]
        [MaxLength(CardToughnessMaxLength)]
        public string Toughness { get; set; } = string.Empty;

        [Comment("This card is part of which expansion.")]
        public int CardGameExpansionId { get; set; }

        public CardGameExpansion CardGameExpansion { get; set; }

        [Comment("Has this card been created during an event?")]
        public bool IsEventCard { get; set; }

#nullable enable
        public string? ArtId { get; set; } = string.Empty;

        [Comment("Art from the database that has been used for creating this card.")]
        public virtual Art Art { get; set; }

        [Required]
        public string AddedByMemberId { get; set; } = string.Empty;

        [Comment("Who is the creator of this card")]
        [ForeignKey(nameof(AddedByMemberId))]
        public virtual ApplicationUser AddedByMember { get; set; }

        [Comment("Has this card been approved by admin.")]
        public bool ApprovedByAdmin { get; set; }

        public int? EventId { get; set; }

        [Comment("Has this been created during an event.")]
        public Event Event { get; set; }

        public bool ForMainPage { get; set; }

        public virtual ICollection<ManaInCard> CardMana { get; set; }

        public virtual ICollection<CommentCard> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<CardDeck> DeckOfCards { get; set; }

        public virtual List<Order> CardsInOrder { get; set; }
    }
}

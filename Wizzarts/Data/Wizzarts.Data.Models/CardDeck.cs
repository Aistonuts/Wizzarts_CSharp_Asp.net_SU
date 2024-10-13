namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class CardDeck : BaseDeletableModel<int>
    {
        public CardDeck()
        {
            this.PlayCards = new HashSet<PlayCard>();
        }

        [Required]
        [MaxLength(CardDeckNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(CardDeckDescriptionMaxMaxLength)]
        public string Description { get; set; }

        [MaxLength(CardDeckShippingAddressMaxLength)]
        public string ShippingAddress { get; set; }

        [MaxLength(RemoteImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;

        public int? StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        public int StatusId { get; set; }

        public DeckStatus Status { get; set; }

        [Required]
        public string CreatedByMemberId { get; set; } = string.Empty;

        [Comment("Who is the creator of this card")]
        [ForeignKey(nameof(CreatedByMemberId))]
        public virtual ApplicationUser CreatedByMember { get; set; }

        public int DeckMinimumCards { get; set; }

        public bool HasEventCards { get; set; }

        public int DeckMinimumEventCards { get; set; }

        public bool HasPriority { get; set; }

        public bool IsLocked { get; set; }

        public virtual ICollection<PlayCard> PlayCards { get; set; }


    }
}

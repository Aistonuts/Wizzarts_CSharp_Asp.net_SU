namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.CardsInOrder = new List<PlayCard>();
        }

        [Required]
        [MaxLength(CardDeckNameMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(CardDeckDescriptionMaxMaxLength)]
        public string Description { get; set; }

#nullable enable
        public int? DeckId { get; set; }

        public string DeckImageUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(StoreLocationMaxLength)]
        public string ShippingAddress { get; set; } = string.Empty;

        public DateTime? EstimatedDeliveryDate { get; set; }

        public int OrderStatusId { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public OrderStatus OrderStatus { get; set; }

        public string RecipientId { get; set; } = string.Empty;

        [ForeignKey(nameof(RecipientId))]
        public ApplicationUser Recipient { get; set; }

        public virtual List<PlayCard> CardsInOrder { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
    public class CardDeck : BaseDeletableModel<int>
    {
        public CardDeck()
        {
            this.PlayCards = new HashSet<PlayCard>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShippingAddress { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int StoreId { get; set; }

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

        public ICollection<PlayCard> PlayCards { get; set; }


    }
}

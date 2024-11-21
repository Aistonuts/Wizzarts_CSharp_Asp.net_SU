namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Wizzarts.Data.Common.Models;

    public class OrderStatus : BaseDeletableModel<int>
    {
        public OrderStatus()
        {
            this.DeckOrders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Order> DeckOrders { get; set; }
    }
}

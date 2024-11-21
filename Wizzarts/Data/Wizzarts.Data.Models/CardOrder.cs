namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Wizzarts.Data.Common.Models;

    public class CardOrder : BaseDeletableModel<int>
    {
        public int OrderId { get; set; }

        public string PlayCardId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [ForeignKey(nameof(PlayCardId))]
        public PlayCard PlayCard { get; set; }
    }
}

namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class ManaColor : BaseDeletableModel<int>
    {
        public string Color { get; set; }

        public string Quantity { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public string CardId { get; set; }

        public Card Card { get; set; }
    }
}

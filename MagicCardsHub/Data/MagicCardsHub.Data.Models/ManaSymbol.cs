namespace MagicCardsHub.Data.Models
{
    using MagicCardsHub.Data.Common.Models;

    public class ManaSymbol : BaseDeletableModel<int>
    {
        public string Color { get; set; }

        public int Quantity { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }
    }
}

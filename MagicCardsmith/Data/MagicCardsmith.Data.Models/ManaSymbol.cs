namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class ManaSymbol : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }
    }
}

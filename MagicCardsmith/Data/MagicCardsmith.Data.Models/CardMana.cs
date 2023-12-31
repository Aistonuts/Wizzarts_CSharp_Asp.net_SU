namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class CardMana : BaseDeletableModel<int>
    {
        public string Color { get; set; }

        public string RemoteImageUrl { get; set; }

        public int CardId { get; set; }

        public Card Card { get; set; }
    }
}

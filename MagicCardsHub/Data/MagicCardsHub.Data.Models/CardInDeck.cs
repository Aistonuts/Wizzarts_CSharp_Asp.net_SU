namespace MagicCardsHub.Data.Models
{
    using MagicCardsHub.Data.Common.Models;

    public class CardInDeck : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Expansion { get; set; }

        public string CardRemoteURL { get; set; }

        public string OriginalCardId { get; set; }

        public int ExpansionDeckId { get; set; }

        public ExpansionCardDeck ExpansionDeck { get; set; }
    }
}

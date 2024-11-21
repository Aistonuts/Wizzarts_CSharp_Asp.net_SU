namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Wizzarts.Data.Common.Models;

    public class DeckOfCards : BaseDeletableModel<int>
    {
        public int DeckId { get; set; }

        public string PlayCardId { get; set; }

        [ForeignKey(nameof(DeckId))]
        public CardDeck Deck { get; set; }

        [ForeignKey(nameof(PlayCardId))]
        public PlayCard PlayCard { get; set; }
    }
}

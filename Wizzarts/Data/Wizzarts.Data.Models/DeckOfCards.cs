using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
    public class DeckOfCards : BaseDeletableModel<int>
    {
        public int DeckId { get; set; }

        public string PlayCardId { get; set; }

        [ForeignKey(nameof(DeckId))]
        public CardDeck Deck{ get; set; }

        [ForeignKey(nameof(PlayCardId))]
        public PlayCard PlayCard { get; set; }
    }
}

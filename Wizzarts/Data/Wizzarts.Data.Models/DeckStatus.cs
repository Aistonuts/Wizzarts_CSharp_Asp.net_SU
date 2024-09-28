using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
    public class DeckStatus : BaseDeletableModel<int>
    {
        public DeckStatus()
        {
            this.Decks = new HashSet<CardDeck>();
        }

        public string Name { get; set; }

        public virtual ICollection<CardDeck> Decks { get; set; }
    }
}

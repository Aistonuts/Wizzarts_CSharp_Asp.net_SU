namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;
    using System;
    using System.Collections.Generic;

    public class Card : BaseDeletableModel<string>
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string CardType { get; set; }

        public string ColorUrl { get; set; }

        public string SymbolUrl { get; set; }

        public int Power { get; set; }

        public int Toughness { get; set; }

        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public string SetOfCardId { get; set; }

        public SetOfCards SetOfCards { get; set; }

        public virtual ICollection<ManaSymbol> ManaSymbols { get; set; }
    }
}

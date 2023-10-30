namespace MagicCardsHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class Card : BaseDeletableModel<string>
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ManaSymbols = new HashSet<ManaSymbol>();
            this.VariousSymbols = new HashSet<VarSymbols>();
        }

        public string Name { get; set; }

        public string FrameColorUrl { get; set; }

        public string SymbolUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public int Power { get; set; }

        public int Toughness { get; set; }

        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public string ExpansionId { get; set; }

        public SetOfCards Expansion { get; set; }

        public virtual ICollection<ManaSymbol> ManaSymbols { get; set; }

        public virtual ICollection<VarSymbols> VariousSymbols { get; set; }

    }
}

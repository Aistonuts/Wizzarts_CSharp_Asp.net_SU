namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Card : BaseDeletableModel<string>
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ManaColors = new HashSet<ManaColor>();
 
        }

        public string Name { get; set; } 

        public string CardRemoteUrl { get; set; }

        public string FrameColorUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public string CardSmithId { get; set; }

        public virtual ApplicationUser CardSmith { get; set; }

        public virtual int GameExpansionId { get; set; }

        public GameExpansion GameExpansion { get; set; }

        public virtual ICollection<ManaColor> ManaColors { get; set; }
    }
}

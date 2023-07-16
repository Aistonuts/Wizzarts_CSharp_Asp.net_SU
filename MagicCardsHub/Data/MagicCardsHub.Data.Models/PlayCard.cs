namespace MagicCardsHub.Data.Models
{
    using System;

    using MagicCardsHub.Data.Common.Models;

    public class PlayCard : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CardType { get; set; }

        public int Damage { get; set; }

        public int Defence { get; set; }

        public int GameFormatId { get; set; }

        public virtual GameFormatProject GameFormat { get; set; }

        public string ArtId { get; set; }

        public virtual DigitalArt Art { get; set; }
    }
}

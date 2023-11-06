namespace MagicCardsHub.Web.ViewModels.Card
{
    using System;

    public abstract class BaseCreateCardByArtIdInputModel
    {
        public string ArtId { get; set; }

        public string ArtTitle { get; set; }

        public string ArtDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string Frame { get; set; }

        public string ManaRedCost { get; set; }

        public string ManaBlueCost { get; set; }

        public string ManaGreenCost { get; set; }

        public string ManaBlackCost { get; set; }

        public string ManaWhiteCost { get; set; }

        public string ManaColorelessCost { get; set; }

        public string CardType { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public string CardAbilitiesFlavor { get; set; }

        public string CardPower { get; set; }

        public string CardToughness { get; set; }
    }
}

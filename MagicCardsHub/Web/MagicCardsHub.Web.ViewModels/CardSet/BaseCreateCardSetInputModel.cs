namespace MagicCardsHub.Web.ViewModels.CardSet
{
    using System.Collections.Generic;

    using MagicCardsHub.Data.Models;

    public abstract class BaseCreateCardSetInputModel
    {
        public string Expansion { get; set; }

        public string Description { get; set; }

        public int NumberOfCards { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}

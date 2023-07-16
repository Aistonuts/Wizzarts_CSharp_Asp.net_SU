namespace MagicCardsHub.Data.Models
{
    using System;

    using MagicCardsHub.Data.Common.Models;

    public class DigitalArt : BaseDeletableModel<string>
    {
        public DigitalArt()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ArtIstId { get; set; }

        public ApplicationUser Artist { get; set; }

        public string PlayCardId { get; set; }

        public virtual PlayCard PlayCard { get; set; }
    }
}

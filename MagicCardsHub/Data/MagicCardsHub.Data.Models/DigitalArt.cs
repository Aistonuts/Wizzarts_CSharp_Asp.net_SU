namespace MagicCardsHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using MagicCardsHub.Data.Common.Models;

    public class DigitalArt : BaseDeletableModel<string>
    {
        public DigitalArt()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
        }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ArtIstId { get; set; }

        public ApplicationUser Artist { get; set; }

        public string PlayCardId { get; set; }

        public virtual PlayCard PlayCard { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

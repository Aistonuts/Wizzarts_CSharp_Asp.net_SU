namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Art : BaseDeletableModel<string>
    {
        public Art()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Articles = new HashSet<Article>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public string AddedById { get; set; }

        public ApplicationUser AddedBy { get; set; }

        public string ArtIstId { get; set; }

        public Artist Artist { get; set; }

        public int? CardId { get; set; }

        public Card? Card { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}

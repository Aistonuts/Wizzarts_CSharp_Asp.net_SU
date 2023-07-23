namespace MagicCardsHub.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public Article()
        {
            this.Images = new HashSet<Image>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ArticleCreatorId { get; set; }

        public ApplicationUser ArticleCreator { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

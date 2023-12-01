namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string? ArtId { get; set; }

        public Art? Art { get; set; }

        public string ArticleCreatorId { get; set; }

        public ApplicationUser ArticleCreator { get; set; }
    }
}

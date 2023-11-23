namespace MagicCardsHub.Data.Models
{
    using MagicCardsHub.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }

        public string? ArtId { get; set; }

        public Art? Art { get; set; }

        public string ArticleCreatorId { get; set; }

        public ApplicationUser ArticleCreator { get; set; }
    }
}

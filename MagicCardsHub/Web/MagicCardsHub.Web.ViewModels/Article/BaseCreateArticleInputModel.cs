namespace MagicCardsHub.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseCreateArticleInputModel
    {
        public string ArtId { get; set; }

        [MinLength(3)]
        public string Title { get; set; }

        [MinLength(3)]
        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }
    }
}
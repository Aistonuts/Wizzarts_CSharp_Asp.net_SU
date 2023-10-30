namespace MagicCardsHub.Web.ViewModels.Art
{
    using MagicCardsHub.Data.Models;

    public abstract class BaseCreateArtInputModel
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }
    }
}

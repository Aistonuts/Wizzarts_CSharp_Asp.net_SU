namespace MagicCardsmith.Web.ViewModels.Art
{
    using MagicCardsmith.Data.Models;

    public abstract class BaseCreateArtInputModel
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }
    }
}

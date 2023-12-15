namespace MagicCardsmith.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;

    public class IndexViewModel
    {
        public IEnumerable<IndexPageArticleViewModel> Articles { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<ArtistInListViewModel> Artists { get; set; }
    }
}

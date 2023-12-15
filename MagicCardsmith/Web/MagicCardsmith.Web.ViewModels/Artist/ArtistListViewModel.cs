namespace MagicCardsmith.Web.ViewModels.Artist
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels;

    public class ArtistListViewModel : PagingViewModel
    {
        public IEnumerable<ArtistInListViewModel> Artists { get; set; }
    }
}

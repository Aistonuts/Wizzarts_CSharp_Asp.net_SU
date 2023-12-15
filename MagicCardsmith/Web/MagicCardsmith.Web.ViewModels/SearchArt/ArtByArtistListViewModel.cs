namespace MagicCardsmith.Web.ViewModels.SearchArt
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels.Art;

    public class ArtByArtistListViewModel : PagingViewModel
    {
        public IEnumerable<ArtInListViewModel> Art { get; set; }
    }
}

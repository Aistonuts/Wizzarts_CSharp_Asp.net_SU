namespace MagicCardsmith.Web.ViewModels.Art
{
    using System.Collections.Generic;

    using MagicCardsHub.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Art;

    public class ArtListViewModel : PagingViewModel
    {
        public IEnumerable<ArtInListViewModel> Art { get; set; }
    }
}

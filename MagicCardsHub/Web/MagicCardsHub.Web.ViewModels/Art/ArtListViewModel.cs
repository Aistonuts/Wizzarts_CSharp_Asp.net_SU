namespace MagicCardsHub.Web.ViewModels.Art
{
    using System.Collections.Generic;

    using MagicCardsHub.Web.ViewModels.Art;

    public class ArtListViewModel : PagingViewModel
    {
        public IEnumerable<ArtInListViewModel> Art { get; set; }
    }
}

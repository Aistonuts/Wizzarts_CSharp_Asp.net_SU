namespace MagicCardsmith.Web.ViewModels.Art
{
    using System.Collections.Generic;

    public class ArtListViewModel : PagingViewModel
    {
        public IEnumerable<ArtInListViewModel> Art { get; set; }
    }
}

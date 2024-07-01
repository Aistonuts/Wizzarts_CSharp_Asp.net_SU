namespace Wizzarts.Web.ViewModels.Art
{
    using System.Collections.Generic;
    using Wizzarts.Web.ViewModels.Home;

    public class ArtListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<ArtInListViewModel> Art { get; set; }
    }
}

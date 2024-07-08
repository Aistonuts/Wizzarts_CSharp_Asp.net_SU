namespace Wizzarts.Web.ViewModels.Store
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class StoreListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<StoreInListViewModel> Stores { get; set; }
    }
}

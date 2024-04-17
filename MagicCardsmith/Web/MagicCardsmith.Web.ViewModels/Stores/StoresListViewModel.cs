namespace MagicCardsmith.Web.ViewModels.Stores
{
    using System.Collections.Generic;

    public class StoresListViewModel : PagingViewModel
    {
        public IEnumerable<StoresInListViewModel> Stores { get; set; }
    }
}

namespace MagicCardsmith.Web.ViewModels.Stores
{
    using System.Collections.Generic;

    public class StoresListViewModel
    {
        public IEnumerable<StoresInListViewModel> Stores { get; set; }
    }
}

namespace Wizzarts.Web.ViewModels.Order
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class OrderListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<OrderInListViewModel> Orders { get; set; }
    }
}

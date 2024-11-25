namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.Order;

    public class MemberDataViewModel : IndexAuthenticationViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public IEnumerable<OrderInListViewModel> Orders { get; set; }
    }
}
namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class MembersListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<MembersInListViewModel> Members { get; set; }

        public IEnumerable<MembersInListViewModel> Artists { get; set; }

        public IEnumerable<MembersInListViewModel> PremiumUsers { get; set; }
    }
}

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class WizzartsTeamListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<WizzartsTeamInListViewModel> WizzartsTeam { get; set; }
    }
}

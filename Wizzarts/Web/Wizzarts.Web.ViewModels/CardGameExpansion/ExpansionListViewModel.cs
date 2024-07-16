namespace Wizzarts.Web.ViewModels.CardGameExpansion
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.Home;

    public class ExpansionListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<ExpansionInListViewModel> CardGameExpansions { get; set; }
    }
}

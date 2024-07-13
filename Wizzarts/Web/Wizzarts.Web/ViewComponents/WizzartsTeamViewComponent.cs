namespace Wizzarts.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class WizzartsTeamViewComponent : ViewComponent
    {
        private readonly IWizzartsServices wizzartsServices;

        public WizzartsTeamViewComponent(
            IWizzartsServices wizzartsServices)
        {
            this.wizzartsServices = wizzartsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new WizzartsTeamListViewModel
            {
                wizzartsTeam = this.wizzartsServices.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>(),
            };
            return View(viewModel);
        }
    }
}

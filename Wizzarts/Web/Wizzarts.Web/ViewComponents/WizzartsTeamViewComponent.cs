namespace Wizzarts.Web.ViewComponents
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class WizzartsTeamViewComponent : ViewComponent
    {
        private readonly IWizzartsServices wizzartsServices;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public WizzartsTeamViewComponent(
            IWizzartsServices wizzartsServices,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.wizzartsServices = wizzartsServices;
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await this.userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)this.User);
            
            var viewModel = new WizzartsTeamListViewModel
            {
                wizzartsTeam = this.wizzartsServices.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>(),
            };
            if (user != null)
            {
                viewModel.CountOfArts = this.userService.GetCountOfArt(user.Id);
                viewModel.CountOfArticles = this.userService.GetCountOfArticles(user.Id);
                viewModel.CountOfCards = this.userService.GetCountOfCards(user.Id);
                viewModel.CountOfEvents = this.userService.GetCountOfEvents(user.Id);
            }

            return this.View(viewModel);
        }
    }
}

namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.GameRules;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.GlobalConstants;
    using static Wizzarts.Common.MessageConstants;
    using static Wizzarts.Common.NotificationMessagesConstants;

    public class WizzartsController : BaseController
    {
        private readonly IWizzartsServices wizzartsServices;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public WizzartsController(
            IWizzartsServices wizzartsServices,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.wizzartsServices = wizzartsServices;
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult GetRules()
        {
            var wizzartsGame = new WizzartsCardGameViewModel()
            {
                CardGameRules = this.wizzartsServices.GetGameRules<GameRulesViewModel>(),
                GameRulesData = this.wizzartsServices.GetAllGameRulesData<GameRulesDataViewModel>(),
                WizzartsTeamMembers = this.wizzartsServices.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>(),
            };

            return this.View(wizzartsGame);
        }

        public async Task<IActionResult> Membership()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var view = new MembershipViewModel();

            if (user == null)
            {
                view.IsMember = false;
            }

            if (user != null)
            {

                var countOfArts = this.userService.GetCountOfArt(this.User.GetId());

                var countOfArticles = this.userService.GetCountOfArticles(this.User.GetId());

                var countOfEvents = this.userService.GetCountOfEvents(this.User.GetId());

                var userRole = await this.userManager.GetRolesAsync(user);

                if (userRole.Contains(MemberRoleName))
                {
                    view.IsMember = true;
                }

                if (userRole.Contains(ArtistRoleName))
                {
                    view.IsArtist = true;
                }

                if (userRole.Contains(ContentCreatorRoleName))
                {
                    view.IsPremiumUser = true;
                }

                if (view.IsMember)
                {
                    view.CurrentRole = GlobalConstants.MemberRoleName;

                    view.ArtNeeded = view.ArtistRoleNeededArts - countOfArts;

                    view.EventsNeeded = view.AllRolesEvents - countOfEvents;

                    view.ArticlesNeeded = view.AllRolesRequiredArticles - countOfArticles;
                }

                if (view.IsArtist)
                {
                    view.CurrentRole = GlobalConstants.ArtistRoleName;

                    view.ArtNeeded = view.PremiumRoleNeededArts - countOfArts;

                    view.EventsNeeded = view.AllRolesEvents - countOfEvents;

                    view.ArticlesNeeded = view.AllRolesRequiredArticles - countOfArticles;
                }

                if (view.IsPremiumUser)
                {
                    view.CurrentRole = GlobalConstants.ContentCreatorRoleName;
                }
            }

            return this.View(view);
        }

        [HttpPost]
        public async Task<IActionResult> GetPremium()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userRole = await this.userManager.GetRolesAsync(user);

            if (userRole.Contains(ContentCreatorRoleName))
            {
                this.ModelState.AddModelError("Error", "You already have premium access");
                this.TempData[ErrorMessage] = "You already have premium access";

                return this.RedirectToAction("Index", "Home");
            }

            var countOfArts = this.userService.GetCountOfArt(user.Id);

            var countOfArticles = this.userService.GetCountOfArticles(user.Id);

            var countOfEvents = this.userService.GetCountOfEvents(user.Id);

            if ((userRole.Contains(ArtistRoleName) && countOfArts > 10) || (userRole.Contains(ArtistRoleName) && countOfArts > 3) || (userRole.Contains(StoreOwnerRoleName) && countOfArticles > 1 && countOfEvents > 1) || (countOfArticles > 5 && countOfEvents > 5) || (userRole.Contains(StoreOwnerRoleName) && userRole.Contains(ArtistRoleName)))
            {
                await this.userManager.AddToRoleAsync(user, ContentCreatorRoleName);
            }
            else
            {
                this.ModelState.AddModelError("Error", NotEligibleForPremium);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Become()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userRole = await this.userManager.GetRolesAsync(user);

            if (userRole.Contains(ArtistRoleName))
            {
                this.ModelState.AddModelError("Error", "You are already an artist");
                this.TempData[ErrorMessage] = "You are already an artist!";

                return this.RedirectToAction("Index", "Home");
            }

            await this.userManager.AddToRoleAsync(user, ArtistRoleName);

            await this.userManager.RemoveFromRoleAsync(user, MemberRoleName);

            return this.RedirectToAction("Index", "Home");
        }
    }
}

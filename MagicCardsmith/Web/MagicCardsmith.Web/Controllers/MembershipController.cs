namespace MagicCardsmith.Web.Controllers
{
    using System.Threading.Tasks;
    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Premium;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static MagicCardsmith.Common.GlobalConstants;
    using static MagicCardsmith.Common.MessageConstants;
    using static MagicCardsmith.Common.NotificationMessagesConstants;

    public class MembershipController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public MembershipController(
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
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

                var countOfArts = this.userService.GetCountOfArt(user.Id);

                var countOfArticles = this.userService.GetCountOfArticles(user.Id);

                var countOfEvents = this.userService.GetCountOfEvents(user.Id);

                var userRole = await this.userManager.GetRolesAsync(user);

                if (userRole.Contains(UserRoleName))
                {
                    view.IsMember = true;
                }

                if (userRole.Contains(ArtistRoleName))
                {
                    view.IsArtist = true;
                }

                if (userRole.Contains(PremiumAccountRoleName))
                {
                    view.IsPremiumUser = true;
                }

                if (view.IsMember)
                {
                    view.CurrentRole = GlobalConstants.UserRoleName;

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
                    view.CurrentRole = GlobalConstants.PremiumAccountRoleName;
                }
            }

            return this.View(view);
        }

        [HttpPost]
        public async Task<IActionResult> GetPremium()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userRole = await this.userManager.GetRolesAsync(user);

            if (userRole.Contains(PremiumAccountRoleName))
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
                await this.userManager.AddToRoleAsync(user, PremiumAccountRoleName);
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

            await this.userManager.RemoveFromRoleAsync(user, UserRoleName);

            return this.RedirectToAction("Index", "Home");
        }
    }
}

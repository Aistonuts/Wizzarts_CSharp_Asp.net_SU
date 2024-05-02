namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Premium;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PremiumController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public PremiumController(
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult Membership()
        {
            return this.View();
        }

        //public async Task<IActionResult> Avatar ()
        //{

        //}

        //public IActionResult Become()
        //{
        //    var viewModel = new ClaimViewModel();
        //    viewModel.AvatarListViewModels = this.userService.GetAllAvatars<AvatarInListViewModel>();
        //    return this.View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> BecomeAsync(BecomeArtistViewModel input)
        //{
        //    var user = await this.userManager.GetUserAsync(this.User);

        //    var userRole = await this.userManager.GetRolesAsync(user);

        //    if (userRole.Contains(GlobalConstants.ArtistRoleName))
        //    {
        //        return this.BadRequest();
        //    }

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(input);
        //    }
        //}
    }
}

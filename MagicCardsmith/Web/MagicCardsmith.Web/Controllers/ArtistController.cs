namespace MagicCardsmith.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.Infrastructure.Extensions;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static MagicCardsmith.Common.NotificationMessagesConstants;

    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArtistController(
            IArtistService artistService,
            UserManager<ApplicationUser> userManager)
        {
            this.artistService = artistService;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId!);
            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeArtistViewModel model)
        {
            string? userId = this.User.GetId();

            bool isAgent = await artistService.ArtistExistsByUserIdAsync(userId);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool userHasArtPieces = await artistService
                .HasArtByUserIdAsync(userId);
            if (!userHasArtPieces)
            {
                TempData[ErrorMessage] = "You must  have at least one art piece in order to  become an artist!";

                return RedirectToAction("Create", "Art");
            }

            try
            {
                await artistService.Create(userId, model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as an agent! Please try again later or contact administrator.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("All", "Art");
        }


        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new ArtistListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.artistService.GetCount(),
                Artists = this.artistService.GetAll<ArtistInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var artist = this.artistService.GetById<SingleArtistViewModel>(id);
            return this.View(artist);
        }
    }
}

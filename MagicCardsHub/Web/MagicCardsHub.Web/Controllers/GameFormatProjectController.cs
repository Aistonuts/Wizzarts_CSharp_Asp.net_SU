namespace MagicCardsHub.Web.Controllers
{
    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels.DigitalArt;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;
    using MagicCardsHub.Web.ViewModels.GameProject;

    public class GameFormatProjectController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGameProjectService gameProjectService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public GameFormatProjectController(
            IGameProjectService gameProjectService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.gameProjectService = gameProjectService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGameProjectInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.gameProjectService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/Images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Digital Art added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Home");
        }
    }
}

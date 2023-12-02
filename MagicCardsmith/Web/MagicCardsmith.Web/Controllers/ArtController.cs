using MagicCardsmith.Data.Models;
using MagicCardsmith.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MagicCardsmith.Services.Data;
using MagicCardsmith.Web.ViewModels.Art;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using MagicCardsmith.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MagicCardsmith.Web.Controllers
{
    public class ArtController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Artist> artistRepository;

        public ArtController(
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Artist> artistRepository)
        {
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.artistRepository = artistRepository;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateArtInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var artist = await this.artistRepository.All().FirstOrDefaultAsync(x=> x.UserId == user.Id);

            try
            {
                await this.artService.CreateAsync(input, artist.Id, $"{this.environment.WebRootPath}/Images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Art added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ArtListViewModel
            {
                Art = this.artService.GetAll<ArtInListViewModel>(id, 3),
            };

            return this.View(viewModel);
        }
    }
}

namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Art;
    using Microsoft.AspNetCore.Identity;
    using Wizzarts.Data.Models;
    using Microsoft.AspNetCore.Hosting;

    public class ArtController : BaseController
    {
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArtController(
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArtViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.artService.AddAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
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
            const int ItemsPerPage = 10;
            var viewModel = new ArtListViewModel
            {
                Art = this.artService.GetRandom<ArtInListViewModel>(20),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var art = this.artService.GetById<SingleArtViewModel>(id);
            //if (information != art.GetInformation())
            //{
            //    return this.BadRequest(information);
            //}

            return this.View(art);
        }
    }
}

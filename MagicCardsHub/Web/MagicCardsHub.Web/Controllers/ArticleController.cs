namespace MagicCardsHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels.Art;
    using MagicCardsHub.Web.ViewModels.Article;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IArticleService articleService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticleController(
            IArticleService articleService,
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.articleService = articleService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Create(string id)
        {
            var art = this.articleService.GetArtById<SingleArtViewModel>(id);
            var viewModel = new CreateArticleInputModel();
            viewModel.ImageUrl = art.ImageUrl;
            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(CreateArticleInputModel input, string id)
        {
            var art = this.articleService.GetArtById<SingleArtViewModel>(id);

            if (!this.ModelState.IsValid)
            {
                input.ArtId = art.Id;
                input.ImageUrl = art.ImageUrl;
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.articleService.CreateAsync(input, user.Id, art.Id, art.ImageUrl);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.ArtId = art.Id;
                input.ImageUrl = art.ImageUrl;
                return this.View(input);
            }

            this.TempData["Message"] = "Article added successfully.";

            return this.RedirectToAction("All");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ArticleListViewModel
            {
                Articles = this.articleService.GetAll<ArticleInListViewModel>(id, 3),
            };

            return this.View(viewModel);
        }
    }
}

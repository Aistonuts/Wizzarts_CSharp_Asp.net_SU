namespace MagicCardsHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels.Article;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IArticlesService articleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticleController(
            IArticlesService articleService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.articleService = articleService;
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
        public async Task<IActionResult> Create(CreateArticleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.articleService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/Images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Article added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Home");
        }
    }
}

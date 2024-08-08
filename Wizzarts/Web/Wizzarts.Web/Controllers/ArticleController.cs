namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Article;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticleController(
            IArticleService articleService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.articleService = articleService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditArticleViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.articleService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpGet]
        public IActionResult Create()
        {

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleViewModel input)
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
                await this.articleService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Article added successfully.";

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ById(int id)
        {
            var article = this.articleService.GetById<SingleArticleViewModel>(id);
            article.Articles = this.articleService.GetRandom<ArticleInListViewModel>(3);

            return this.View(article);
        }
    }
}

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
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticleController(
            IArticleService articleService,
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.articleService = articleService;
            this.userService = userService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.articleService.ArticleExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.articleService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

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

            if (await this.articleService.ArticleExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.articleService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.articleService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id, information = input.GetArticleTitle() });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateArticleViewModel
            {
                Articles = this.articleService.GetRandom<ArticleInListViewModel>(5),
            };
            return this.View(viewModel);
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

            bool isPremium = await this.userService.IsPremium(this.User.GetId());
            try
            {
                await this.articleService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images", isPremium);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Article added successfully.";

            return this.RedirectToAction("MyData", "User");
        }

        public async Task<IActionResult> ById(int id, string information)
        {
            var article = await this.articleService.GetById<SingleArticleViewModel>(id);
            if (article == null || information != article.GetArticleTitle())
            {
                return this.BadRequest();
            }

            if (article != null)
            {
                article.Articles = this.articleService.GetRandom<ArticleInListViewModel>(3);
            }

            return this.View(article);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveArticle(int id)
        {
            var userId = await this.articleService.ApproveArticle(id);
            if (userId != null)
            {
                return this.RedirectToAction("ById", "Member", new { id = $"{userId}", Area = "Administration" });
            }
            else
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await this.articleService.ArticleExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.articleService.HasUserWithIdAsync(id, this.User.GetId()) == false
                || this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.articleService.DeleteAsync(id);

            return this.RedirectToAction("MyData", "User");
        }
    }
}

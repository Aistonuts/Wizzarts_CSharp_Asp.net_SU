namespace MagicCardsHub.Web.Controllers
{
    using System.Diagnostics;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels;
    using MagicCardsHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;

        public HomeController(IArticleService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                RandomArticles = this.articlesService.GetRandom<IndexPageArticleViewModel>(4),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

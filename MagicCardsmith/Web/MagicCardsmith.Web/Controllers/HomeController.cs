namespace MagicCardsmith.Web.Controllers
{
    using System.Diagnostics;

    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Home;
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
                Articles = this.articlesService.GetRandom<IndexPageArticleViewModel>(3),
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

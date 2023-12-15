namespace MagicCardsmith.Web.Controllers
{
    using System.Diagnostics;

    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IArtistService artistService;

        public HomeController(
            IArticleService articlesService,
            IArtService artService,
            IArtistService artistService)
        {
            this.articlesService = articlesService;
            this.artService = artService;
            this.artistService = artistService;

        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Articles = this.articlesService.GetRandom<IndexPageArticleViewModel>(3),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Artists = this.artistService.GetRandom<ArtistInListViewModel>(4),
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

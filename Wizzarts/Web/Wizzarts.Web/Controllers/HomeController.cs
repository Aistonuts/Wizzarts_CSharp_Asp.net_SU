namespace Wizzarts.Web.Controllers
{
    using System.Diagnostics;

    using Wizzarts.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Services.Data;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.Article;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IPlayCardService playCardService;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IPlayCardExpansionService cardExpansionService;

        public HomeController(
           IArticleService articlesServic,
           IArtService artService,
           IPlayCardService playCardService,
           IEventService eventService,
           IStoreService storeService,
           IPlayCardExpansionService cardExpansionService)
        {
            this.articlesService = articlesServic;
            this.artService = artService;
            this.playCardService = playCardService;
            this.eventService = eventService;
            this.storeService = storeService;
            this.cardExpansionService = cardExpansionService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Articles = this.articlesService.GetRandom<ArticleInListViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Cards = this.playCardService.GetRandom<CardInListViewModel>(3),
                Stores = this.storeService.GetAll<StoresInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.cardExpansionService.GetAll<ExpansionInListViewModel>(),
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

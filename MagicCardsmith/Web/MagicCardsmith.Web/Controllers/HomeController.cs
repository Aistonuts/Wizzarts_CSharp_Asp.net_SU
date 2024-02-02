namespace MagicCardsmith.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Home;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Mvc;
    using System.Web;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IArtistService artistService;
        private readonly ICardService cardService;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IExpansionService expansionService;

        public HomeController(
            IArticleService articlesService,
            IArtService artService,
            IArtistService artistService,
            ICardService cardService,
            IEventService eventService,
            IStoreService storeService,
            IExpansionService expansionService)
        {
            this.articlesService = articlesService;
            this.artService = artService;
            this.artistService = artistService;
            this.cardService = cardService;
            this.eventService = eventService;
            this.storeService = storeService;
            this.expansionService = expansionService;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexViewModel
            {
                Articles = this.articlesService.GetRandom<IndexPageArticleViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Artists = this.artistService.GetRandom<ArtistInListViewModel>(4),
                Cards = this.cardService.GetRandom<CardInListViewModel>(3),
                Stores = this.storeService.GetAll<StoresInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.expansionService.GetAll<ExpansionInListViewModel>(),
                //CardTypes = this.cardService.GetAllTypes<CardTypeIdViewModel>(),
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

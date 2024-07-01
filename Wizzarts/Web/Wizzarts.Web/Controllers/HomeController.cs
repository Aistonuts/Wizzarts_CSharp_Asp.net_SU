namespace Wizzarts.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexAuthenticationViewModel> _logger;
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IPlayCardService playCardService;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IPlayCardExpansionService cardExpansionService;
        private readonly IMemoryCache cache;

        public HomeController(
           SignInManager<ApplicationUser> _signInManager,
           ILogger<IndexAuthenticationViewModel> _logger,
           IArticleService articlesServic,
           IArtService artService,
           IPlayCardService playCardService,
           IEventService eventService,
           IStoreService storeService,
           IPlayCardExpansionService cardExpansionService,
           IMemoryCache cache)
        {
            this._signInManager = _signInManager;
            this._logger = _logger;
            this.articlesService = articlesServic;
            this.artService = artService;
            this.playCardService = playCardService;
            this.eventService = eventService;
            this.storeService = storeService;
            this.cardExpansionService = cardExpansionService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexAuthenticationViewModel()
            {
                Articles = this.articlesService.GetRandom<ArticleInListViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Cards = this.playCardService.GetRandom<CardInListViewModel>(4),
                Stores = this.storeService.GetAll<StoresInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.cardExpansionService.GetAll<ExpansionInListViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexAuthenticationViewModel loginModel, string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this._signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    this._logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View();
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

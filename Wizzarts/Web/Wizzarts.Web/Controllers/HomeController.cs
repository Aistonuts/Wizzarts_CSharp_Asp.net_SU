namespace Wizzarts.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.Store;
    using Wizzarts.Data;
    using System.Linq;
    using Wizzarts.Web.ViewModels.Chat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using static Wizzarts.Common.GlobalConstants;
    using Wizzarts.Web.Infrastructure.Extensions;

    public class HomeController : BaseController
    {
        public const int GeneralChatId = 1;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IChatService chatService;
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IPlayCardService playCardService;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IPlayCardExpansionService cardExpansionService;
        private readonly IMemoryCache cache;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
           SignInManager<ApplicationUser> _signInManager,
           ILogger<HomeController> _logger,
           IChatService chatService,
           IArticleService articlesServic,
           IArtService artService,
           IPlayCardService playCardService,
           IEventService eventService,
           IStoreService storeService,
           IPlayCardExpansionService cardExpansionService,
           IUserService userService,
           IMemoryCache cache,
           UserManager<ApplicationUser> userManager)
        {
            this._signInManager = _signInManager;
            this._logger = _logger;
            this.chatService = chatService;
            this.articlesService = articlesServic;
            this.artService = artService;
            this.playCardService = playCardService;
            this.eventService = eventService;
            this.storeService = storeService;
            this.cardExpansionService = cardExpansionService;
            this.userService = userService;
            this.cache = cache;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            string welcomeMessage = " ";

            if (user != null)
            {
                var currentRole = await this.userManager.GetRolesAsync(user);
                if (!currentRole.Contains(AdministratorRoleName))
                {
                    welcomeMessage = await this.userService.UpdateRoleAsync(user, user.Id);
                }
            }
            else
            {
                welcomeMessage = string.Empty;
            }

            var viewModel = new IndexAuthenticationViewModel()
            {
                Articles = this.articlesService.GetRandom<ArticleInListViewModel>(8),
                Arts = this.artService.GetRandom<ArtInListViewModel>(4),
                Cards = this.playCardService.GetRandom<CardInListViewModel>(4),
                Stores = this.storeService.GetAll<StoreInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.cardExpansionService.GetAll<ExpansionInListViewModel>(),
                ChatMessages = this.chatService.GetAllGeneralChatMessages<DbChatMessagesInListViewModel>(GeneralChatId),
                ChatRooms = this.chatService.GetAllChatRooms<SingleChatViewModel>(),
                ChatId = GeneralChatId,
                MembershipStatus = welcomeMessage,
                CountOfArts = this.userService.GetCountOfArt(this.User.GetId()),
                CountOfArticles = this.userService.GetCountOfArticles(this.User.GetId()),
                CountOfCards = this.userService.GetCountOfCards(this.User.GetId()),
                CountOfEvents = this.userService.GetCountOfEvents(this.User.GetId()),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(IndexAuthenticationViewModel loginModel, string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            var viewModel = new IndexAuthenticationViewModel()
            {
                Articles = this.articlesService.GetRandom<ArticleInListViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Cards = this.playCardService.GetRandom<CardInListViewModel>(4),
                Stores = this.storeService.GetAll<StoreInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.cardExpansionService.GetAll<ExpansionInListViewModel>(),
                ChatMessages = this.chatService.GetAllGeneralChatMessages<DbChatMessagesInListViewModel>(GeneralChatId),
            };

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
                    return this.View(viewModel);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }


        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return this.View("Error400");
            }

            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return View();
            //return this.View(
            //    new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

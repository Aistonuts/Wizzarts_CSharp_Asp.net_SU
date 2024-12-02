namespace Wizzarts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.GameRules;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.GlobalConstants;

    public class WizzartsController : BaseController
    {
        private readonly IWizzartsServices wizzartsServices;
        private readonly IUserService userService;
        private readonly IArtService artService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;
        private readonly IPlayCardService cardService;

        private readonly UserManager<ApplicationUser> userManager;

        public WizzartsController(
            IWizzartsServices wizzartsServices,
            IUserService userService,
            IArtService artService,
            IEventService eventService,
            IArticleService articleService,
            IPlayCardService cardService,
            UserManager<ApplicationUser> userManager)
        {
            this.wizzartsServices = wizzartsServices;
            this.userService = userService;
            this.artService = artService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.cardService = cardService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetRules()
        {
            var wizzartsGame = new WizzartsCardGameViewModel()
            {
                CardGameRules = await this.wizzartsServices.GetGameRules<GameRulesViewModel>(),
                GameRulesData = await this.wizzartsServices.GetAllGameRulesData<GameRulesDataViewModel>(),
                WizzartsTeamMembers = await this.wizzartsServices.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>(),
            };

            return this.View(wizzartsGame);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Membership()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var view = new MembershipViewModel();

            if (user != null)
            {
                var countOfArts = this.userService.GetCountOfArt(this.User.GetId());

                var countOfArticles = this.userService.GetCountOfArticles(this.User.GetId());

                var countOfEvents = this.userService.GetCountOfEvents(this.User.GetId());

                var countOfCards = this.userService.GetCountOfCards(this.User.GetId());

                if (this.User.IsMember() == true && this.User.IsArtist() == false && this.User.IsPremiumUser() == false && this.User.IsAdmin() == false)
                {
                    view.ArtNeeded = countOfArts;

                    view.EventsNeeded = countOfEvents;

                    view.ArticlesNeeded = countOfArticles;

                    view.CardsNeeded = countOfCards;
                }

                if (this.User.IsMember() && this.User.IsPremiumUser() && this.User.IsArtist() == false && this.User.IsAdmin() == false)
                {
                    view.ArtNeeded = countOfArts;
                }
            }

            return this.View(view);
        }

        public async Task<IActionResult> ById(int id)
        {
            var userId = this.wizzartsServices.GetUserIdByArtistId(id);
            if (userId == null)
            {
                return this.BadRequest();
            }

            var member = await this.userService.GetById<SingleMemberViewModel>(userId);
            member.Arts = await this.artService.GetAllArtByUserId<ArtInListViewModel>(userId, 1, 50);
            member.Articles = await this.articleService.GetAllArticlesByUserId<ArticleInListViewModel>(userId, 1, 50);
            member.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(userId, 1, 50);
            return this.View(member);
        }
    }
}

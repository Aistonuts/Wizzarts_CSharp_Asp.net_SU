namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
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
    using static Wizzarts.Common.MessageConstants;
    using static Wizzarts.Common.NotificationMessagesConstants;

    public class WizzartsController : BaseController
    {
        private readonly IWizzartsServices wizzartsServices;
        private readonly IUserService userService;
        private readonly IArtService artService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;

        private readonly UserManager<ApplicationUser> userManager;

        public WizzartsController(
            IWizzartsServices wizzartsServices,
            IUserService userService,
            IArtService artService,
            IEventService eventService,
            IArticleService articleService,
            UserManager<ApplicationUser> userManager)
        {
            this.wizzartsServices = wizzartsServices;
            this.userService = userService;
            this.artService = artService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult GetRules()
        {
            var wizzartsGame = new WizzartsCardGameViewModel()
            {
                CardGameRules = this.wizzartsServices.GetGameRules<GameRulesViewModel>(),
                GameRulesData = this.wizzartsServices.GetAllGameRulesData<GameRulesDataViewModel>(),
                WizzartsTeamMembers = this.wizzartsServices.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>(),
            };

            return this.View(wizzartsGame);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Membership()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var view = new MembershipViewModel();

            if (user == null)
            {
                view.IsMember = false;
            }

            if (user != null)
            {

                var countOfArts = this.userService.GetCountOfArt(this.User.GetId());

                var countOfArticles = this.userService.GetCountOfArticles(this.User.GetId());

                var countOfEvents = this.userService.GetCountOfEvents(this.User.GetId());

                var userRole = await this.userManager.GetRolesAsync(user);

                if (userRole.Contains(MemberRoleName))
                {
                    view.IsMember = true;
                }

                if (userRole.Contains(ArtistRoleName))
                {
                    view.IsArtist = true;
                }

                if (userRole.Contains(PremiumRoleName))
                {
                    view.IsPremiumUser = true;
                }

                if (view.IsMember)
                {
                    view.CurrentRole = GlobalConstants.MemberRoleName;

                    view.ArtNeeded = view.ArtistRoleNeededArts - countOfArts;

                    view.EventsNeeded = view.AllRolesEvents - countOfEvents;

                    view.ArticlesNeeded = view.AllRolesRequiredArticles - countOfArticles;
                }

                if (view.IsArtist)
                {
                    view.CurrentRole = GlobalConstants.ArtistRoleName;

                }

                if (view.IsPremiumUser)
                {
                    view.CurrentRole = GlobalConstants.PremiumRoleName;
                }
            }

            return this.View(view);
        }

        public IActionResult ById(int id)
        {
            var userId = this.wizzartsServices.GetUserIdByArtistId(id);
            var member = this.userService.GetById<SingleMemberViewModel>(userId);
            member.Arts = this.artService.GetAllArtByUserId<ArtInListViewModel>(userId, 1, 50);
            member.Articles = this.articleService.GetAllArticlesByUserId<ArticleInListViewModel>(userId, 1, 50);
            member.Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(userId, 1, 50);
            return this.View(member);
        }
    }
}

namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Order;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.HardCodedConstants;

    public class UserController : BaseController
    {
        private readonly IArtService artService;
        private readonly IDeckService deckService;
        private readonly IOrderService orderService;
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IPlayCardService cardService;
        private readonly IStoreService storeService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
           IArtService artService,
           IDeckService deckService,
           IOrderService orderService,
           IArticleService articleService,
           IUserService userService,
           IEventService eventService,
           IPlayCardService cardService,
           IStoreService storeService,
           UserManager<ApplicationUser> userManager)
        {
            this.artService = artService;
            this.deckService = deckService;
            this.orderService = orderService;
            this.articleService = articleService;
            this.userService = userService;
            this.eventService = eventService;
            this.cardService = cardService;
            this.storeService = storeService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> SelectAvatar()
        {
            var viewModel = new AvatarListViewModel
            {
                Avatars = await this.userService.GetAllAvatars<AvatarInListViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                id = DefaultUserAvatarId;
            }

            var avatar = await this.userService.GetAvatarById<CreateMemberProfileViewModel>(id);
            avatar.Avatars = await this.userService.GetAllAvatars<AvatarInListViewModel>();
            avatar.AvatarId = id;
            return this.View(avatar);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateMemberProfileViewModel input)
        {
            input.Avatars = await this.userService.GetAllAvatars<AvatarInListViewModel>();
            if (await this.userService.NickNameExist(input.Nickname))
            {
                this.ModelState.AddModelError(nameof(input.Nickname), "Nickname already exist!");
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            if (!this.ModelState.IsValid)
            {
                input.Avatars = await this.userService.GetAllAvatars<AvatarInListViewModel>();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.userService.UpdateAsync(this.User.GetId(), input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Avatars = await this.userService.GetAllAvatars<AvatarInListViewModel>();
                return this.View(input);
            }

            this.TempData["Message"] = "User profile has been updated successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyData(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var view = new MemberDataViewModel
            {
                Nickname = user.Nickname,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
            };
            view.Arts = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());
            view.Articles = await this.articleService.GetAllArticlesByUserIdPageless<ArticleInListViewModel>(this.User.GetId());
            view.Events = await this.eventService.GetAllEventsByUserIdPageless<EventInListViewModel>(this.User.GetId());
            view.Cards = await this.cardService.GetAllCardsByUserIdPageless<CardInListViewModel>(this.User.GetId());
            view.Stores = await this.storeService.GetAllStoresByUserIdPageless<StoreInListViewModel>(this.User.GetId());
            view.Decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            view.Orders = await this.orderService.GetAllOrdersByUserId<OrderInListViewModel>(this.User.GetId());
            return this.View(view);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var members = this.userManager.GetUsersInRoleAsync(GlobalConstants.MemberRoleName).Result;
            var contentCreators = this.userManager.GetUsersInRoleAsync(GlobalConstants.PremiumRoleName).Result;
            var artists = this.userManager.GetUsersInRoleAsync(GlobalConstants.ArtistRoleName).Result;
            const int ItemsPerPage = 12;

            var viewModel = new MembersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,

                Artists = artists.Select(x => new MembersInListViewModel
                {
                    Nickname = x.Nickname,
                    Username = x.UserName,
                    Bio = x.Bio,
                    AvatarUrl = x.AvatarUrl,
                    Role = GlobalConstants.ArtistRoleName,
                    CountOfArticles = this.userService.GetCountOfArticles(x.Id),
                    CountOfArts = this.userService.GetCountOfArt(x.Id),
                    CountOfEvents = this.userService.GetCountOfEvents(x.Id),
                    CountOfCards = this.userService.GetCountOfCards(x.Id),
                }),
                Members = members.Select(x => new MembersInListViewModel
                {
                    Nickname = x.Nickname,
                    Username = x.UserName,
                    Bio = x.Bio,
                    AvatarUrl = x.AvatarUrl,
                    Role = GlobalConstants.MemberRoleName,
                    CountOfArticles = this.userService.GetCountOfArticles(x.Id),
                    CountOfArts = this.userService.GetCountOfArt(x.Id),
                    CountOfEvents = this.userService.GetCountOfEvents(x.Id),
                    CountOfCards = this.userService.GetCountOfCards(x.Id),
                }),
                PremiumUsers = contentCreators.Select(x => new MembersInListViewModel
                {
                    Nickname = x.Nickname,
                    Username = x.UserName,
                    Bio = x.Bio,
                    AvatarUrl = x.AvatarUrl,
                    Role = GlobalConstants.PremiumRoleName,
                    CountOfArticles = this.userService.GetCountOfArticles(x.Id),
                    CountOfArts = this.userService.GetCountOfArt(x.Id),
                    CountOfEvents = this.userService.GetCountOfEvents(x.Id),
                    CountOfCards = this.userService.GetCountOfCards(x.Id),
                }),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(string id, string information)
        {
            if (id == null)
            {
                return this.Unauthorized();
            }

            var member = await this.userService.GetByUserName<SingleMemberViewModel>(id);

            if (member == null || information != member.GetWizzartsMemberName())
            {
                return this.BadRequest();
            }

            member.Arts = await this.artService.GetAllArtByUserId<ArtInListViewModel>(id, 1, 50);
            member.Articles = await this.articleService.GetAllArticlesByUserId<ArticleInListViewModel>(id, 1, 50);
            member.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(id, 1, 50);
            return this.View(member);
        }
    }
}

using MagicCardsmith.Web.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagicCardsmith.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.Areas.Administration.Models.Users;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Premium;
    using MagicCardsmith.Web.ViewModels.Stores;
    using MagicCardsmith.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private readonly IArtistService artistService;
        private readonly IArtService artService;
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly ICardService cardService;
        private readonly IStoreService storeService;
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
            IArtistService artistService,
            IArtService artService,
            IArticleService articleService,
            IUserService userService,
            IEventService eventService,
            ICardService cardService,
            IStoreService storeService,
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            this.artistService = artistService;
            this.artService = artService;
            this.articleService = articleService;
            this.userService = userService;
            this.eventService = eventService;
            this.cardService = cardService;
            this.storeService = storeService;
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        public IActionResult Avatar()
        {
            var viewModel = new AvatarListViewModel
            {
                Avatars = this.userService.GetAllAvatars<AvatarInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Update(int id)
        {
            var avatar = this.userService.GetAvatarById<CreateProfileViewModel>(id);
            avatar.Avatars = this.userService.GetAllAvatars<AvatarInListViewModel>();
            return this.View(avatar);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateProfileViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.userService.UpdateAsync(user.Id, input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "User profile has been updated successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Mine(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            const int ItemsPerPage = 10;

            var view = new MyDataUserViewModel
            {
                Nickname = user.Nickname,
                Id = user.Id,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
            };
            view.Arts = this.artService.GetAllByUserId<ArtInListViewModel>(user.Id, id, ItemsPerPage);
            view.Articles = this.articleService.GetAllByUserId<ArticleInListViewModel>(user.Id, id, ItemsPerPage);
            view.Events = this.eventService.GetAllByUserId<EventInListViewModel>(user.Id, id, ItemsPerPage);
            view.Cards = this.cardService.GetAllByUserId<CardInListViewModel>(user.Id, id, ItemsPerPage);
            view.Stores = this.storeService.GetAllByUserId<StoresInListViewModel>(user.Id, id, ItemsPerPage);
            view.CardReviews = this.reviewService.GetAllByUserId<CardReviewInListViewModel>(user.Id, id, ItemsPerPage);
            return this.View(view);
        }
    }
}
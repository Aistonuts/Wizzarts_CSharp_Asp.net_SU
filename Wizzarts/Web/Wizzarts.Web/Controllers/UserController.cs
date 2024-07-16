using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Wizzarts.Data.Models;
using Wizzarts.Services.Data;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Store;
using Wizzarts.Web.ViewModels.WizzartsMember;

namespace Wizzarts.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IArtService artService;
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IPlayCardService cardService;
        private readonly IStoreService storeService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
           IArtService artService,
           IArticleService articleService,
           IUserService userService,
           IEventService eventService,
           IPlayCardService cardService,
           IStoreService storeService,
           UserManager<ApplicationUser> userManager)
        {
            this.artService = artService;
            this.articleService = articleService;
            this.userService = userService;
            this.eventService = eventService;
            this.cardService = cardService;
            this.storeService = storeService;
            this.userManager = userManager;
        }

        public IActionResult SelectAvatar()
        {
            var viewModel = new AvatarListViewModel
            {
                Avatars = this.userService.GetAllAvatars<AvatarInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Update(int id)
        {
            if(id == 0)
            {
                id = 1;
            }
            var avatar = this.userService.GetAvatarById<CreateMemberProfileViewModel>(id);
            avatar.Avatars = this.userService.GetAllAvatars<AvatarInListViewModel>();
            avatar.AvatarId = id;
            return this.View(avatar);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateMemberProfileViewModel input)
        {
            input.Avatars = this.userService.GetAllAvatars<AvatarInListViewModel>();
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
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

        public async Task<IActionResult> MyData(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            const int ItemsPerPage = 10;

            var view = new MemberDataViewModel
            {
                Nickname = user.Nickname,
                Id = user.Id,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
            };
            view.Arts = this.artService.GetAllArtByUserId<ArtInListViewModel>(user.Id, id, ItemsPerPage);
            view.Articles = this.articleService.GetAllArticlesByUserId<ArticleInListViewModel>(user.Id, id, ItemsPerPage);
            view.Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(user.Id, id, ItemsPerPage);
            view.Cards = this.cardService.GetAllCardsByUserId<CardInListViewModel>(user.Id, id, ItemsPerPage);
            view.Stores = this.storeService.GetAllStoresByUserId<StoreInListViewModel>(user.Id, id, ItemsPerPage);
            return this.View(view);
        }
    }
}

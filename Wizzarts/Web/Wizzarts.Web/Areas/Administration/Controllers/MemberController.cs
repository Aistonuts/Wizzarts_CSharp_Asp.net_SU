namespace Wizzarts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Areas.Administration.Models.User;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;

    public class MemberController : AdministrationController
    {
        private readonly IArtService artService;
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IPlayCardService cardService;
        private readonly IStoreService storeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache cache;

        public MemberController(
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

        [Route("[controller]/[action]")]
        public async Task<IActionResult> All()
        {
            var users = this.userManager.GetUsersInRoleAsync(GlobalConstants.MemberRoleName).Result;
            var premiumUser = this.userManager.GetUsersInRoleAsync(GlobalConstants.PremiumRoleName).Result;
            var admins = this.userManager.GetUsersInRoleAsync(GlobalConstants.AdministratorRoleName).Result;
            var artists = this.userManager.GetUsersInRoleAsync(GlobalConstants.ArtistRoleName).Result;
            var wizzarts = this.userManager.GetUsersInRoleAsync(GlobalConstants.WizzartsTeamRoleName).Result;
            var filtered = new List<ApplicationUser>();
            foreach (var item in users)
            {
                if (await this.userManager.IsInRoleAsync(item, GlobalConstants.AdministratorRoleName)
                    || await this.userManager.IsInRoleAsync(item, GlobalConstants.ArtistRoleName)
                    || await this.userManager.IsInRoleAsync(item, GlobalConstants.PremiumRoleName)
                    || await this.userManager.IsInRoleAsync(item, GlobalConstants.WizzartsTeamRoleName))
                {
                    continue;
                }

                filtered.Add(item);
            }

            var viewModel = new UserListViewModelAdminArea
            {
                Users = filtered.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                    Name = x.UserName,
                }),

                Premium = premiumUser.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                    Name = x.UserName,
                }),

                Artists = artists.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                    Name = x.UserName,
                }),

                Wizzarts = wizzarts.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                    Name = x.UserName,
                }),

                Admins = admins.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                    Name = x.UserName,
                    Phone = x.PhoneNumber,
                }),
            };

            return this.View(viewModel);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> ById(string id)
        {
            var users = this.userManager.FindByIdAsync(id).Result;

            var user = new SingleUserViewModelAdminArea
            {
                Nickname = users.Nickname,
                Id = users.Id,
                Email = users.Email,
                AvatarUrl = users.AvatarUrl,

            };

            user.Arts = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(user.Id);
            user.Articles = await this.articleService.GetAllArticlesByUserIdPageless<ArticleInListViewModel>(user.Id);
            user.Events = await this.eventService.GetAllEventsByUserIdPageless<EventInListViewModel>(user.Id);
            user.Cards = await this.cardService.GetAllCardsByUserIdPageless<CardInListViewModel>(user.Id);
            user.Stores = await this.storeService.GetAllStoresByUserIdPageless<StoreInListViewModel>(user.Id);
            return this.View(user);
        }
    }
}

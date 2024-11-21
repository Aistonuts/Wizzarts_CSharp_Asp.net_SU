namespace Wizzarts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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

    using static Wizzarts.Common.AdminConstants;

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
            UserManager<ApplicationUser> userManager,
            IMemoryCache cache)
        {
            this.artService = artService;
            this.articleService = articleService;
            this.userService = userService;
            this.eventService = eventService;
            this.cardService = cardService;
            this.storeService = storeService;
            this.userManager = userManager;
            this.cache = cache;
        }

        [Route("[controller]/[action]")]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;

            var users = this.cache
                .Get<IEnumerable<ApplicationUser>>(UsersCacheKey);

            if (users == null)
            {
                users = this.userManager.GetUsersInRoleAsync(GlobalConstants.MemberRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(UsersCacheKey, users, cacheOptions);
            }

            var premiumUser = this.cache
               .Get<IEnumerable<ApplicationUser>>(PremiumCacheKey);

            if (premiumUser == null)
            {
                premiumUser = this.userManager.GetUsersInRoleAsync(GlobalConstants.PremiumRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(PremiumCacheKey, premiumUser, cacheOptions);
            }

            var admins = this.cache
               .Get<IEnumerable<ApplicationUser>>(AdminsCacheKey);

            if (admins == null)
            {
                admins = this.userManager.GetUsersInRoleAsync(GlobalConstants.AdministratorRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(AdminsCacheKey, admins, cacheOptions);
            }
 
            var artists = this.cache
              .Get<IEnumerable<ApplicationUser>>(ArtistsCacheKey);

            if (artists == null)
            {
                artists = this.userManager.GetUsersInRoleAsync(GlobalConstants.ArtistRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(ArtistsCacheKey, artists, cacheOptions);
            }

            var viewModel = new UserListViewModelAdminArea
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,

                Users = users.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),


                Premium = premiumUser.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                Artists = artists.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                Admins = admins.Select(x => new UserInListViewModelAdminArea
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(string id, int idPage = 1)
        {
            if (idPage <= 0)
            {
                return this.NotFound();
            }

            var users = this.userManager.FindByIdAsync(id).Result;

            const int ItemsPerPage = 5;

            var user = new SingleUserViewModelAdminArea
            {
                Nickname = users.Nickname,
                Id = users.Id,
                Email = users.Email,
                AvatarUrl = users.AvatarUrl,
                ItemsPerPage = ItemsPerPage,
                PageNumber = idPage,
            };
            user.Arts = this.artService.GetAllArtByUserId<ArtInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Articles = this.articleService.GetAllArticlesByUserId<ArticleInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Cards = this.cardService.GetAllCardsByUserId<CardInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Stores = this.storeService.GetAllStoresByUserId<StoreInListViewModel>(user.Id, idPage, ItemsPerPage);
            return this.View(user);
        }
    }
}

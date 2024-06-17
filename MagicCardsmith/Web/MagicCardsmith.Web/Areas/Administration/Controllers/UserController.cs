namespace MagicCardsmith.Web.Areas.Administration.Controllers
{
    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.Areas.Administration.Models.Users;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static MagicCardsmith.Common.AdminConstants;


    public class UserController : AdministrationController
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
        private readonly IMemoryCache cache;

        public UserController(
            IArtistService artistService,
            IArtService artService,
            IArticleService articleService,
            IUserService userService,
            IEventService eventService,
            ICardService cardService,
            IStoreService storeService,
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager,
            IMemoryCache cache)
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
            this.cache = cache;
        }

        [Route("User/All")]
        public async Task<IActionResult> All(int id = 1)
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
                users = this.userManager.GetUsersInRoleAsync(GlobalConstants.UserRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(UsersCacheKey, users, cacheOptions);
            }

            var storeOwners = this.cache
               .Get<IEnumerable<ApplicationUser>>(StoreOwnersCacheKey);

            if (storeOwners == null)
            {
                storeOwners = this.userManager.GetUsersInRoleAsync(GlobalConstants.StoreOwnerRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(StoreOwnersCacheKey, storeOwners, cacheOptions);
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

            var premium = this.cache
              .Get<IEnumerable<ApplicationUser>>(PremiumCacheKey);

            if (premium == null)
            {
                premium = this.userManager.GetUsersInRoleAsync(GlobalConstants.PremiumAccountRoleName).Result;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(PremiumCacheKey, premium, cacheOptions);
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

            var viewModel = new UserServiceListModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,

                Users = users.Select(x => new UserServiceInListModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                Artists = artists.Select(x => new UserServiceInListModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                Admins = admins.Select(x => new UserServiceInListModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                StoreOwners = storeOwners.Select(x => new UserServiceInListModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    AvatarUrl = x.AvatarUrl,
                }),

                Premium = premium.Select(x => new UserServiceInListModel
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

            var user = new SingleUserViewModel
            {
                Nickname = users.Nickname,
                Id = users.Id,
                Email = users.Email,
                AvatarUrl = users.AvatarUrl,
                ItemsPerPage = ItemsPerPage,
                PageNumber = idPage,
            };
            user.Arts = this.artService.GetAllByUserId<ArtInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Articles = this.articleService.GetAllByUserId<ArticleInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Events = this.eventService.GetAllByUserId<EventInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Cards = this.cardService.GetAllByUserId<CardInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.Stores = this.storeService.GetAllByUserId<StoresInListViewModel>(user.Id, idPage, ItemsPerPage);
            user.CardReviews = this.reviewService.GetAllByUserId<CardReviewInListViewModel>(user.Id, idPage, ItemsPerPage);
            return this.View(user);
        }
    }
}
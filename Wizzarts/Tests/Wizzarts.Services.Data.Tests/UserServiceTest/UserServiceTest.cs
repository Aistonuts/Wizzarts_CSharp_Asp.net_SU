using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.WizzartsMember;
using Xunit;

namespace Wizzarts.Services.Data.Tests.UserServiceTest
{
    public class UserServiceTest : UnitTestBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStoreService storeService;
        public UserServiceTest(
            UserManager<ApplicationUser> userManager,
            IStoreService storeService)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.userManager = userManager;
            this.storeService = storeService;
        }

        [Fact]
        public async Task UserGetByIdShouldReturnTheCorrectUserName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent= new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar= new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            var currentUser = service.GetById<SingleMemberViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");

            Assert.Equal("Drawgoon", currentUser.UserName);
        }

        [Fact]
        public async Task ArtAllArtByUserIdShouldReturnTheCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            var artByUserDrawgoon = service.GetAllArtByUserId<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            Assert.Equal(8, artByUserDrawgoon.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetArtCountByUserIdShouldShouldReturnTheCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            int artCount = service.GetCountOfArt("2738e787-5d57-4bc7-b0d2-287242f04695");
            Assert.Equal(8, artCount);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAvatarShouldReturnCorrectCountAndTheFirstOneShouldHaveTheCorrectTitle()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            var avatars = service.GetAllAvatars<AvatarInListViewModel>();

            var avatar = data.Avatars.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(6, avatars.Count());
            Assert.Equal("Marvel One",avatar.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetCountOfArticlesByIdShouldReturnCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            int articles = service.GetCountOfArticles("2b346dc6-5bd7-4e64-8396-15a064aa27a7");

            Assert.Equal(6, articles);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetCountOfEventsByIdShouldReturnCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            int events = service.GetCountOfEvents("2738e787-5d57-4bc7-b0d2-287242f04695");

            Assert.Equal(4, events);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAvatarByIdShouldReturnTheCorrectAvatar()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            var avatars = service.GetAvatarById<AvatarInListViewModel>(1);

            Assert.Equal("Marvel One", avatars.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task UpdateUserProfileShouldSetTheUserNameTONewName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, this.userManager, repositoryUser, this.storeService);

            var newUserData = new CreateMemberProfileViewModel
            {
                Nickname = "Test",
                AvatarUrl = "test",
                Bio = "test",
                AvatarId = 2,
            };

            await service.UpdateAsync("2738e787-5d57-4bc7-b0d2-287242f04695", newUserData);

            var userNewData = data.Users.FirstOrDefault(x => x.Id == "2738e787-5d57-4bc7-b0d2-287242f04695");

            Assert.Equal(newUserData.Nickname, userNewData.Nickname);
            Assert.Equal(newUserData.AvatarId, userNewData.AvatarId);
        }
    }
}

namespace Wizzarts.Services.Data.Tests.UserServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.WizzartsMember;
    using Xunit;

    public class UserServiceTest : UnitTestBase
    {
        public UserServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task UserGetByIdShouldReturnTheCorrectUserName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            var currentUser = await service.GetById<SingleMemberViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");

            Assert.Equal("Drawgoon", currentUser.UserName);
            this.TearDownBase();
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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            var artByUserDrawgoon = await service.GetAllArtByUserId<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            var avatars = await service.GetAllAvatars<AvatarInListViewModel>();

            var avatar = data.Avatars.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(6, avatars.Count());
            Assert.Equal("Marvel One", avatar.Name);
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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            var avatars = await service.GetAvatarById<AvatarInListViewModel>(1);

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
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

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

        [Fact]
        public async Task HasNickNameShouldReturnTrueWhenUserWithNickNameIsSelected()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            Assert.True(await service.HasNickName("2738e787-5d57-4bc7-b0d2-287242f04695"));
            this.TearDownBase();
        }

        [Fact]
        public async Task HasNickNameShouldReturnFalseWhenUserWithoutNickNameIsSelected()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            Assert.False(await service.HasNickName("0ac1e577-c7ff-4aa3-83c3-e5acac9de281"));
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_User_Id_By_Its_Name_Should_Return_Correect_Data()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, null, repositoryUser, fileService);

            var currentUserId = await service.GetMemberIdByUserName("Drawgoon");

            Assert.Equal("2738e787-5d57-4bc7-b0d2-287242f04695", currentUserId);
            this.TearDownBase();
        }
    }
}

namespace Wizzarts.Services.Data.Tests.UserServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
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
        public async Task User_GetById_Should_Return_The_Correct_UserName()
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
        public async Task GetAllArtByUserId_Should_Return_The_Correct_Count()
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
        public async Task GetArtCount_ByUserId_Should_Should_Return_The_Correct_Count()
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
        public async Task GetAllAvatars_Should_Return_Correct_Count_And_The_First_One_Should_Have_The_Correct_Title()
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
        public async Task Get_Count_Of_Articles_ById_Should_Return_Correct_Count()
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
        public async Task GetCountOfEvents_ById_Should_Return_Correct_Count()
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
        public async Task GetAvatarById_Should_Return_The_Correct_Avatar()
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
        public async Task Update_UserProfile_Should_Set_The_UserName_To_New_Name()
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
        public async Task Update_UserProfile_With_Special_Should_Set_The_UserName_To_New_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            userManagerMock
                .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            userManagerMock
                .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, userManagerMock.Object, repositoryUser, fileService);

            var newUserData = new CreateMemberProfileViewModel
            {
                Nickname = "AdminAndy",
                AvatarUrl = "test",
                PhoneNumber = "012285695439",
                Bio = "faef3ddf-05e3-4bd3-9753-5401e2053c75",
                AvatarId = 2,
            };

            await service.UpdateAsync("2893eb26-f4de-4cb5-8acf-17e1888efe1e", newUserData);

            var userNewData = data.Users.FirstOrDefault(x => x.Id == "2893eb26-f4de-4cb5-8acf-17e1888efe1e");

            if (userNewData != null)
            {
                Assert.Equal("Andy", userNewData.Nickname);
                Assert.Equal("Traveling from town to town", userNewData.Bio);
                Assert.Equal("0111234567", userNewData.PhoneNumber);
            }
        }

        [Fact]
        public async Task HasNickName_Should_Return_True_When_User_With_NickName_Is_Selected()
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
        public async Task HasNickName_Should_Return_False_When_User_Without_NickName_Is_Selected()
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
        public async Task Get_User_Id_By_Its_Name_Should_Return_Correct_Data()
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

        [Fact]
        public async Task UpdateRoleAsync_Should_Return_Correct_Messages()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            userManagerMock
                .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            userManagerMock
                .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);
            using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(data);
            var fileService = new FileService();

            var currentUser = await data.Users.FirstOrDefaultAsync(x => x.Id == "2738e787-5d57-4bc7-b0d2-287242f04695");
            var service = new UserService(repositoryArt, repositoryArticle, repositoryPlayCard, repositoryEvent, repositoryAvatar, userManagerMock.Object, repositoryUser, fileService);

            var rolesOne = new List<string> { "Member" };
 
            var rolesThree = new List<string> { "Member" };
            var resultOne = await service.UpdateRoleAsync(currentUser, "2738e787-5d57-4bc7-b0d2-287242f04695", rolesOne);
            Assert.Equal("You have acquired the artist role.", resultOne);
            this.TearDownBase();
        }
    }
}

using Microsoft.AspNetCore.Http;
using MyTested.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Data;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.WizzartsMember;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class UserControllerTest : UnitTestBase
    {
        [Fact]
        public void SelectAvatarhouldReturnViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
             .Instance(instance => instance
                 .WithData(data.Avatars.ToList()))
             .Calling(c => c.SelectAvatar())
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<AvatarListViewModel>());

            TearDownBase();
        }

        [Fact]
        public void UpdateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                .Instance(instance => instance
                 .WithUser()
                 .WithData(data.Avatars))
             .Calling(c => c.Update(With.Any<int>()))
             .ShouldHave()
             .ActionAttributes(attrs => attrs
                 .RestrictingForHttpMethod(HttpMethod.Get))

             .AndAlso()
             .ShouldReturn()
             .View();
            TearDownBase();
        }

        [Fact]
        public void UpdatePostShouldUpdateUserProfileAndSetTempDataMessageAndRedirectWhenValidModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);

            var service = new UserService(repositoryArt, repositoryArticle, repositoryEvent, repositoryAvatar, repositoryUser);

            MyController<UserController>
                .Instance(instance => instance
                 .WithUser(X=>X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
                 .WithData(data.Avatars)
                 .WithData(data.Users))
               .Calling(c => c.Update(
                   new CreateMemberProfileViewModel
                   {
                       Nickname = "Test",
                       AvatarUrl = "test",
                       Bio = "test",
                       AvatarId = 2,
                       Avatars = service.GetAllAvatars<AvatarInListViewModel>(),
                   }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("User profile has been updated successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<HomeController>(c => c.Index()));
            TearDownBase();
        }

        //[Fact]
        //public void UpdatePostShouldReturnViewWithSameModelWhenInvalidModelState()
        //{
        //    OneTimeSetup();
        //    var data = this.dbContext;

        //    using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
        //    using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
        //    using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
        //    using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
        //    using var repositoryAvatar = new EfDeletableEntityRepository<Avatar>(data);

        //    var service = new UserService(repositoryArt, repositoryArticle, repositoryEvent, repositoryAvatar, repositoryUser);

        //    MyController<UserController>
        //        .Instance(instance => instance
        //         .WithUser(X => X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
        //         .WithData(data.Avatars)
        //         .WithData(data.Users))
        //      .Calling(c => c.Update(
        //            new CreateMemberProfileViewModel
        //            {
        //                Avatars = service.GetAllAvatars<AvatarInListViewModel>(),
        //            }))
        //      .ShouldHave()
        //      .InvalidModelState()
        //      .AndAlso()
        //      .ShouldReturn()
        //      .View(With.Default<CreateMemberProfileViewModel>());
        //    TearDownBase();
        //}

        [Fact]
        public void UpdatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
          => MyController<UserController>
              .Calling(c => c.Update(With.Default<CreateMemberProfileViewModel>()))
              .ShouldHave()
              .ActionAttributes(attrs => attrs
                  .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void MyDataShouldReturnCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;

          

            MyController<UserController>
                .Instance(instance => instance
                 .WithUser()
                 .WithData(data.Arts)
                 .WithData(data.Articles)
                 .WithData(data.Events)
                 .WithData(data.PlayCards)
                 .WithData(data.Stores))
              .Calling(c => c.MyData(With.No<int>()))
              .ShouldReturn()
              .View(With.Default<MemberDataViewModel>());
        }
    }
}

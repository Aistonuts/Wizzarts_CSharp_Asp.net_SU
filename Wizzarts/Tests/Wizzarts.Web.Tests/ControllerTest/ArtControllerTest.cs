namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Caching.Memory;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Xunit;

    using static Wizzarts.Common.GlobalConstants;

    public class ArtControllerTest : UnitTestBase
    {
        [Fact]
        public void Add_Should_Return_View_With_Correct_Model()
        {
            MyController<ArtController>
            .Calling(c => c.Add(With.Default<AddArtViewModel>())) // Provides a global service.
            .ShouldReturn()
            .View(With.Default<AddArtViewModel>());
        }

        [Fact]
        public void Add_Art_Should_Return_View_With_Same_Model_When_Invalid_Model_State()
            => MyController<ArtController>
                .Calling(c => c.Add(With.Default<AddArtViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<AddArtViewModel>());

        [Theory]
        [InlineData("Art Title", "Art Content")]
        public void Create_Post_Should_Save_Art_Set_TempDataMessage_And_Redirect_When_Valid_Model(string title, string content)
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            MyController<ArtController>
                .Instance(instance => instance
                .WithUser(X => X.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
               .Calling(c => c.Add(new AddArtViewModel
               {
                   Title = title,
                   Description = content,
                   RemoteImageUrl = "test",
                   Image = file,
               }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .Data(data => data
               .WithSet<Art>(set =>
               {
                   set.ShouldNotBeEmpty();
                   set.SingleOrDefault(a => a.Title == title).ShouldNotBeNull();
               }))
               .AndAlso()
               .ShouldHave()
               .TempData(temp => temp.ContainingEntryWithValue("Art added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<UserController>(c => c.MyData(With.No<int>())));

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Get_Should_Return_BadRequest_If_Art_Is_Not_Found()
            => MyMvc.Controller<ArtController>()

                .Calling(c => c.Edit(With.Any<string>()))

                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void Edit_Get_Should_Return_Unauthorized_If_User_Is_Not_The_Art_Owner()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier(With.Any<string>())))
                .Calling(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldReturn()
                .Unauthorized();

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Get_With_The_Id_Of_The_Art_Owner_Should_Return_Correct_View()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
                .Calling(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditArtViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Get_With_Admin_Role_Should_Return_View()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier(With.Any<string>()).AndAlso().InRole(AdministratorRoleName)))
                .Calling(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditArtViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Get_Request_Not_By_The_Owner_And_Without_Admin_Rights_Should_Return_Unauthorized()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier(With.Any<string>()).AndAlso().InRole(MemberRoleName)))
                .Calling(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldReturn()
                .Unauthorized();

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Post_Should_Return_Bad_Request_If_Art_Does_Not_Exist()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
            };

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Articles))
                .Calling(c => c.Edit(
                    model,
                    With.Any<string>()))
                .ShouldReturn()
                .BadRequest();

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Post_Should_Return_View_When_Request_Is_Made_By_The_Owner()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
                Image = file,
            };

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
                .Calling(c => c.Edit(
                    model,
                    "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArtController>(c => c.ById("ab8532f9-2a2f-4b65-96f1-90e5468fbed2", "test")));

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Post_Should_Return_View_When_Request_Is_Made_By_Admin()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
                Image = file,
            };

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier(With.Any<string>()).AndAlso().InRole(AdministratorRoleName)))
                .Calling(c => c.Edit(
                    model,
                    "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArtController>(c => c.ById("ab8532f9-2a2f-4b65-96f1-90e5468fbed2", "test")));

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Post_Should_Have_Invalid_Model_State_When_Model_With_Missing_Data_Is_Being_Provided()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
            };

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier(With.Any<string>()).AndAlso().InRole(AdministratorRoleName)))
                .Calling(c => c.Edit(
                    model,
                    "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(model);

            this.TearDownBase();
        }

        [Fact]
        public void Edit_Post_Should_Return_View_And_Restricted_For_HttpPost()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
            };

            MyController<ArtController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
                .Calling(c => c.Edit(
                    model,
                    "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post));

            this.TearDownBase();
        }
    }
}

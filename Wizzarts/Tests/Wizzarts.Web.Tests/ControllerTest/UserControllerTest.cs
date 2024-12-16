namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.WizzartsMember;
    using Xunit;

    public class UserControllerTest : UnitTestBase
    {
        public UserControllerTest()
        {
        }

        [Fact]
        public void Select_Avatar_Should_Return_View_With_Correct_Model()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
             .Instance(instance => instance
                 .WithData(data.Avatars.ToList()))
             .Calling(c => c.SelectAvatar())
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<AvatarListViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void Update_Get_Should_Have_Restrictions_For_HttpGet_Only_And_Authorized_Users_And_Should_Return_View()
        {
            this.OneTimeSetup();
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
            this.TearDownBase();
        }

        [Fact]
        public void Update_Post_Should_Update_User_Profile_And_Set_Temp_Data_Message_And_Redirect_When_Valid_Model()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                .Instance(instance => instance
                 .WithUser(X => X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
                 .WithData(data.Avatars)
                 .WithData(data.Users))
               .Calling(c => c.Update(
                   new CreateMemberProfileViewModel
                   {
                       Nickname = "Test",
                       AvatarUrl = "test",
                       Bio = "test",
                       PhoneNumber = "12345678",
                       AvatarId = 2,
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
                   .To<HomeController>(c => c.Index(With.No<string>())));
            this.TearDownBase();
        }

        [Fact]
        public void Update_Post_Should_Return_View_When_Invalid_Model_State()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                .Instance(instance => instance
                 .WithUser(X => X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
                 .WithData(data.Avatars)
                 .WithData(data.Users))
               .Calling(c => c.Update(
                   new CreateMemberProfileViewModel
                   {
                       Nickname = "Test",
                       AvatarUrl = "test",
                       Bio = "test",
                       AvatarId = 2,
                   }))
               .ShouldHave()
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
               .View(view => view
                .WithModelOfType<CreateMemberProfileViewModel>());
            this.TearDownBase();
        }


        [Fact]
        public void Update_Post_Should_Have_Restrictions_Fo_rHttp_Post_Only_And_Authorized_Users()
          => MyController<UserController>
              .Calling(c => c.Update(With.Default<CreateMemberProfileViewModel>()))
              .ShouldHave()
              .ActionAttributes(attrs => attrs
                  .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void My_Data_Should_Return_Correct_Model_And_Data()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                   .Instance(instance => instance
                 .WithUser(X => X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
                 .WithData(data.Users))
                 .Calling(c => c.MyData(1))
                 .ShouldReturn()
                  .View(view => view
                .WithModelOfType<MemberDataViewModel>()
                  .Passing(model =>
                  {
                      Assert.Equal("drawgoon@aol.com", model.Email);
                      Assert.Equal(8, model.Arts.Count());
                  }));
            TearDownBase();
        }

        [Fact]
        public void All_Should_Return_Correct_Model_And_Data()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                   .Instance(instance => instance
                 .WithData(data.Users))
                 .Calling(c => c.All(1))
                 .ShouldReturn()
                  .View(view => view
                .WithModelOfType<MembersListViewModel>());
            TearDownBase();
        }

        [Fact]
        public void By_Id_Should_Return_Correct_Model_And_Data()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                   .Instance(instance => instance
                 .WithData(data.Users))
                 .Calling(c => c.ById("Drawgoon", "Drawgoon"))
                 .ShouldReturn()
                  .View(view => view
                .WithModelOfType<SingleMemberViewModel>());
            TearDownBase();
        }

        [Fact]
        public void ById_Should_Return_Bad_Request_When_Bad_In_formation()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                   .Instance(instance => instance
                 .WithData(data.Users))
                 .Calling(c => c.ById("2738e787-5d57-4bc7-b0d2-287242f04695", "Dra"))
                 .ShouldReturn()
                 .BadRequest();
            TearDownBase();
        }

        [Fact]
        public void ById_Should_Return_Bad_Request_When_Non_Existing_User()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<UserController>
                   .Instance(instance => instance
                 .WithData(data.Users))
                 .Calling(c => c.ById("2738e787-5d57-4bc7-b0d2-287242f04694", "Drawgoon"))
                 .ShouldReturn()
                 .BadRequest();
            TearDownBase();
        }
    }
}

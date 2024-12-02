namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.GameRules;
    using Wizzarts.Web.ViewModels.WizzartsMember;
    using Xunit;

    using static Wizzarts.Common.GlobalConstants;

    public class WizzartsControllerTest : UnitTestBase
    {
        [Fact]
        public void Get_Rules_Should_Return_Default_View_With_Correct_Model()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                            .WithData(data.WizzartsGameRules.ToList()))
                        .Calling(c => c.GetRules())
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<WizzartsCardGameViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void Get_Membership_Info_Without_Logged_In_User_Should_Return_View()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                         .WithUser())
                        .Calling(c => c.Membership())
                        .ShouldReturn().
                        View(result => result
                       .WithModelOfType<MembershipViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void Get_Membership_Info_With_Logged_In_Member_Should_Return_View_And_Correct_Data()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                            .WithData(data.Users)
                         .WithUser(x => x.WithIdentifier("4a50fd21-682d-42b8-9b30-168d120ad224").InRole(MemberRoleName)))
                        .Calling(c => c.Membership())
                        .ShouldReturn()
                        .View(result => result
                       .WithModelOfType<MembershipViewModel>()
                       .Passing(model =>
                       {
                           Assert.Equal(3, model.ArtistRoleNeededArts);
                           Assert.Equal(2, model.AllRolesRequiredArticles);
                           Assert.Equal(2, model.AllRolesCards);
                           Assert.Equal(1, model.AllRolesEvents);
                       }));

            this.TearDownBase();
        }

        [Fact]
        public void ById_Should_Return_Bad_Request_If_No_Artist_With_Such_Id()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                            .WithData(data.Users)
                         .WithUser())
                        .Calling(c => c.ById(20))
                        .ShouldReturn()
                       .BadRequest();

            this.TearDownBase();
        }

        [Fact]
        public void ById_Should_Return_View_If_User_With_Such_ArtistId_Exist()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                            .WithData(data.WizzartsTeamMembers)
                        .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
                        .Calling(c => c.ById(1))
                        .ShouldReturn()
                          .View(result => result
                        .WithModelOfType<SingleMemberViewModel>());
            this.TearDownBase();
        }
    }
}

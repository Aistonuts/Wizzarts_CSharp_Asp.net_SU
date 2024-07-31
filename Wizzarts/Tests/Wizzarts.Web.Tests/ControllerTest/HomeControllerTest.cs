using MyTested.AspNetCore.Mvc;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Wizzarts.Data.Models;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Expansion;
using Wizzarts.Web.ViewModels.Home;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Store;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            MyController<HomeController>
            .Instance()
            .Calling(c => c.Index()) // Provides a global service.
            .ShouldReturn()
            .View(result => result
            .WithModelOfType<IndexAuthenticationViewModel>()
            .Passing(x =>
            {
                x.Articles.Count().Equals(6);
                x.Arts.Count().Equals(3);
                x.Cards.Count().Equals(4);
            }));
        }

        [Theory]
        [InlineData("fakeUrl")]
        public void IndexPostShouldReturnViewWithSameModelWhenInvalidModelState(string url)
        {
            MyController<HomeController>
                       .Calling(c => c.Index(With.Default<IndexAuthenticationViewModel>()
                          ,
                           url))
                       .ShouldHave()
                       .ModelState(modelstate => modelstate
                       .For<IndexAuthenticationViewModel>()
                       .ContainingErrorFor(m => m.UserName)
                       .ContainingErrorFor(m => m.Password))
                       .AndAlso()
                        .ShouldReturn()
                        .View();

        }

        [Fact]
        public void PostLoginShouldReturnRedirectToActionWithValidUserName()
        {
            var model = new IndexAuthenticationViewModel
            {
                UserName = SignInManagerMock.ValidUser,
                Password = SignInManagerMock.ValidUser
            };

            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Index(
                    model,
                    With.No<string>()))
                .ShouldReturn()
                .View();


        }
        [Fact]

        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void PrivacyShouldReturnDefaultView()
            => MyController<HomeController>
                .Calling(c => c.Privacy())
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("User@mobile.com", "Passwordaaaaaaaaaaaaaaa", "fakeUrl")]
        public void IndexPostSignInShouldReturnViewWithSameModelWhenInvalidModelState(string username, string password, string url)
        {
            MyController<HomeController>
                .Calling(c => c.Index(
                    new IndexAuthenticationViewModel
                    {
                        UserName = SignInManagerMock.ValidUser,
                        Password = SignInManagerMock.ValidUser
                    }, With.No<string>()))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<ApplicationUser>(set =>
                    {
                        set.ShouldNotBeEmpty();
                        set.SingleOrDefault(a => a.UserName == SignInManagerMock.ValidUser).ShouldNotBeNull();
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey("User logged in."))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));

        }

        [Fact]
        public void PostLoginShouldReturnReturnViewWithInvalidCredentials()
        {
            var model = new IndexAuthenticationViewModel
            {
                UserName = "Invalid@invalid.com",
                Password = "Invalid",
            };

            var returnModel = new IndexAuthenticationViewModel
            {
                Articles = new List<ArticleInListViewModel>(),
                Arts = new List<ArtInListViewModel>(),
                Cards = new List<CardInListViewModel>(),
                Stores = new List<StoreInListViewModel>(),
                Events = new List<EventInListViewModel>(),
                GameExpansions = new List<ExpansionInListViewModel>(),
            };

            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Index(
                    model,
                    With.No<string>()))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<ValidationSummary>()
                    .ContainingError(string.Empty)
                    .ThatEquals("Invalid login attempt."))
                .AndAlso()
                .ShouldReturn()
                .View(returnModel);
        }
    }
}

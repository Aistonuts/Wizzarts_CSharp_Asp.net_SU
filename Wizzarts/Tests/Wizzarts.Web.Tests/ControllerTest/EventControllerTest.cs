using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using System.IO;
using System.Linq;
using System.Text;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Data;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class EventControllerTest : UnitTestBase
    {
        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            MyController<EventController>
                        .Instance(instance => instance
                            .WithData(data.Events.ToList()))
                        .Calling(c => c.All())
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<EventListViewModel>());

            TearDownBase();
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectModel()
        {

            OneTimeSetup();
            var data = this.dbContext;

            MyController<EventController>
             .Instance(instance => instance
                 .WithUser()
                 .WithData(data.Events.FirstOrDefault(x => x.Id == 1)))
             .Calling(c => c.ById(1, With.No<int>()))
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<SingleEventViewModel>());

            TearDownBase();
        }

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
           => MyController<EventController>
               .Calling(c => c.Create())
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get))
               .AndAlso()
               .ShouldReturn()
               .View();

        [Theory]
        [InlineData("Event Title", "Create Event EventEventEventEventEventEventEvent")]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel(string title, string content)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");



            MyController<EventController>
                .Instance()
                .WithUser()
               .Calling(c => c.Create(new CreateEventViewModel
               {
                   Title = title,
                   EventDescription = content,
                   Image = file,
               }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .Data(data => data
               .WithSet<Event>(set =>
               {
                   set.ShouldNotBeEmpty();
                   set.SingleOrDefault(a => a.Title == title).ShouldNotBeNull();
               }))
               .AndAlso()
               .ShouldHave()
               .TempData(temp => temp.ContainingEntryWithValue("Event added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<HomeController>(c => c.Index()));
        }
    }
}

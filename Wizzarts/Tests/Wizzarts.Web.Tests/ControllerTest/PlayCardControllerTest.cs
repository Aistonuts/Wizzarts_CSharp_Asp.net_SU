using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Data;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.CardGameExpansion;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Expansion;
using Wizzarts.Web.ViewModels.PlayCard;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class PlayCardControllerTest : UnitTestBase
    {
        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.Events))
             .Calling(c => c.Create(1))
             .ShouldHave()
             .ActionAttributes(attrs => attrs
                 .RestrictingForHttpMethod(HttpMethod.Get))

             .AndAlso()
             .ShouldReturn()
             .View();
            TearDownBase();
        }

        [Fact]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.Events))
               .Calling(c => c.Create(
                   new CreateCardViewModel
                   {
                       Name = "TestTestTest",
                       CardRemoteUrl = "aaaaa",
                       CardDefaultDescription = "aaaa",
                       BlackManaId = 1,
                       BlueManaId = 1,
                       RedManaId = 1,
                       WhiteManaId = 1,
                       GreenManaId = 2,
                       ColorlessManaId = 1,
                       CardFrameColorId = 1,
                       CardTypeId = 1,
                       AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                       Power = "1",
                       Toughness = "2",
                       ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                       Images = file,
                   }, 1, "captured"))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Card added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<EventController>(c => c.All()));
            TearDownBase();
        }

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Events))
                .Calling(c => c.Create(With.Default<CreateCardViewModel>(), 1, "captured"))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CreateCardViewModel>());
        }

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Events))
                .Calling(c => c.Create(With.Default<CreateCardViewModel>(), 1, "captured"))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post));

        }

        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            MyController<PlayCardController>
                        .Instance(instance => instance
                            .WithData(data.PlayCards.ToList()))
                        .Calling(c => c.All(1))
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<CardListViewModel>());

            TearDownBase();
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
             .Instance(instance => instance
                 .WithUser()
                 .WithData(data.PlayCards.FirstOrDefault(x => x.Id == "c330fecf-61a9-4e03-8052-cd2b9583a251")))
             .Calling(c => c.ById("c330fecf-61a9-4e03-8052-cd2b9583a251", With.No<string>()))
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<SingleCardViewModel>());

            TearDownBase();
        }

        //[Fact]
        //public void ExpansionShouldReturnViewWithCorrectModel()
        //{
        //    OneTimeSetup();
        //    var data = this.dbContext;

        //    MyController<PlayCardController>
        //     .Instance(instance => instance
        //         .WithData(data.CardGameExpansions.ToList()))
        //     .Calling(c => c.Expansion())
        //     .ShouldReturn()
        //     .View(view => view
        //         .WithModelOfType<ExpansionListViewModel>());

        //    TearDownBase();
        //}
    }
}

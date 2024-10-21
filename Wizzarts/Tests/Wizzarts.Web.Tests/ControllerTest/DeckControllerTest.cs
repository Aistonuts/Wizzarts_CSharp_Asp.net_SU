using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using MyTested.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium.DevTools.V123.Input;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Data;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Deck;
using Xunit;
using Wizzarts.Web.ViewModels.Article;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class DeckControllerTest : UnitTestBase
    {
        [Fact]
        public void AddWhenUserHasNoDecksShouldReturnRedirectToActionCreate()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cards = data.PlayCards;
            MyController<DeckController>
                .Calling(c => c.Add(new SingleDeckViewModel()
                {
                    SearchEvent = "Event",
                },With.No<int>()))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Create(With.No<int>())));

            TearDownBase();
        }

        [Fact]
        public void AddWhenUserHasAtLeastOneDeckShouldRedirectToAddView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c=> c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
                .Calling(c => c.Add(new SingleDeckViewModel()
                {
                    SearchEvent = "Event",
                    SearchName = "Ancestral Recall",
                    SearchType = "Instant",
                }, 1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleDeckViewModel>());

            TearDownBase();
        }

        [Fact]
        public void DispatchShouldRedirectToAddCardView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithData(data.CardDecks))
                .Calling(c => c.Dispatch(new SingleDeckViewModel()
                {

                    Id = 1,
                }))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.ById(1)));

            TearDownBase();
        }

        [Fact]
        public void AddCardRedirectToAddView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var input = new SingleDeckViewModel()
            {
                Id = 1,
                SearchEvent = "Event",
                SearchName = "Ancestral Recall",
                SearchType = "Instant",
            };
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.CardDecks))
                .Calling(c => c.AddCard("c330fecf-61a9-4e03-8052-cd2b9583a251", 1))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Add(With.No<SingleDeckViewModel>(),1)));

            TearDownBase();
        }

        [Fact]
        public void RemoveCardRedirectToAddView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.CardDecks))
                .Calling(c => c.Remove("c330fecf-61a9-4e03-8052-cd2b9583a251", 1))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Add(With.No<SingleDeckViewModel>(), 1)));

            TearDownBase();
        }

        [Fact]
        public void CreateDeckShouldHaveRestrictionsForHttpGetOnlyAndShouldReturnView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<DeckController>
                .Calling(c => c.Create(4))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Get))

                .AndAlso()
                .ShouldReturn()
                .View();

            TearDownBase();
        }

        [Fact]
        public void CreateDeckShouldReturnCorrectViewMode()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var model = new CreateDeckViewModel()
            {
            };
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
                .Calling(c => c.Create(With.No<int>()))
                .ShouldReturn()
                .View(view => view.WithModelOfType<CreateDeckViewModel>());

            TearDownBase();
        }

        [Fact]
        public void CreateDeckPostShouldSaveDeckAndSetTempDataMessageAndRedirectWhenValidModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");
            var model = new CreateDeckViewModel()
            {
                Name = "testtesttesttest",
                Description = "TestTestTestTestTestTestTestTestTest",
                ShippingAddress = "testtesttesttesttest",
                Image = file,
                EventId = 4,
            };
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
                .Calling(c => c.Create(model))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithValue("Deck added successfully."))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Create(With.Any<int>())));

            TearDownBase();
        }

        [Fact]
        public void CreateDeckPostShouldReturnViewWithSameModelWhenInvalidModelState()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
                .Calling(c => c.Create(With.Default<CreateDeckViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithModelOfType<CreateDeckViewModel>());

            TearDownBase();
        }

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DeckController>
                .Calling(c => c.Create(With.Default<CreateDeckViewModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithData(data.CardDecks))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view.WithModelOfType<DeckListViewModel>());

            TearDownBase();
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithData(c=>c.WithEntities(data.CardDecks)))
                .Calling(c => c.ById(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<SingleDeckViewModel>());

            TearDownBase();
        }

        [Fact]
        public void ChangeShouldRedirectToAddDeckController()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(c => c.WithEntities(data.CardDecks)))
                .Calling(c => c.Change(1))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Add(With.No<SingleDeckViewModel>(), 1)));

            TearDownBase();
        }

        [Fact]
        public void ChangeShouldRedirectToCreateDeckControllerIfNoDeckFound()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<DeckController>
                .Instance(instance => instance.WithUser(c => c.WithIdentifier("3b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(c => c.WithEntities(data.CardDecks)))
                .Calling(c => c.Change(1))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DeckController>(c => c.Create(With.No<int>())));

            TearDownBase();
        }
    }
}

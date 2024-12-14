namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Xunit;

    using static Wizzarts.Common.GlobalConstants;

    public class PlayCardControllerTest : UnitTestBase
    {
        [Fact]
        public void AddGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7").InRole(AdministratorRoleName))
                 .WithData(data.Users))
             .Calling(c => c.Add(0))
             .ShouldHave()
             .ActionAttributes(attrs => attrs
                 .RestrictingForHttpMethod(HttpMethod.Get))
             .AndAlso()
             .ShouldReturn()
               .View(view => view
                .WithModelOfType<CreateCardViewModel>());
            this.TearDownBase();
        }

        [Fact]
        public void AddGetShouldHaveRestrictionsForMemberRoleAndShouldReturnUnauthorized()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7").InRole(MemberRoleName))
                 .WithData(data.Users))
             .Calling(c => c.Add(0))
             .ShouldReturn()
             .Unauthorized();
            this.TearDownBase();
        }

        [Fact]
        public void AddGetShouldRedirectToAddNewArtIfUserHasNoArts()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("4a50fd21-682d-42b8-9b30-168d120ad224").InRole(ArtistRoleName))
                 .WithData(data.Users))
             .Calling(c => c.Add(0))
             .ShouldReturn()
              .RedirectToAction("Add", "Art");
            this.TearDownBase();
        }

        [Fact]
        public void AddPostShouldSaveArtSetTempDataMessageAndRedirectWhenValidModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.PlayCards))
               .Calling(c => c.Add(
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
                       CardTypeId = 2,
                       AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                       Power = "1",
                       Toughness = "2",
                       ArtId = "d4381a9a-094d-4695-b938-9fdbc8e3a35c",
                       Images = file,
                   }, "captured"))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Card added successfully."))
               .AndAlso()
               .ShouldReturn()
               .RedirectToAction("Add", "PlayCard");

            this.TearDownBase();
        }

        [Fact]
        public void AddPostShouldDetectInvalidModelStateWhenArtIsDoesNotExist()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.PlayCards))
               .Calling(c => c.Add(
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
                       CardTypeId = 2,
                       AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                       Power = "1",
                       Toughness = "2",
                       ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                       Images = file,
                   }, "captured"))
               .ShouldHave()
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
                 .View(view => view
                .WithModelOfType<CreateCardViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void AddPostShouldDetectInvalidModelStateWhenCategoriesAreMissing()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.PlayCards))
               .Calling(c => c.Add(
                   new CreateCardViewModel
                   {
                       Name = "TestTestTest",
                       CardRemoteUrl = "aaaaa",
                       CardDefaultDescription = "aaaa",
                       BlackManaId = 20,
                       BlueManaId = 1,
                       RedManaId = 1,
                       WhiteManaId = 1,
                       GreenManaId = 2,
                       ColorlessManaId = 1,
                       CardFrameColorId = 1,
                       CardTypeId = 2,
                       AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                       Power = "1",
                       Toughness = "2",
                       ArtId = "d4381a9a-094d-4695-b938-9fdbc8e3a35c",
                       Images = file,
                   }, "captured"))
               .ShouldHave()
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
                 .View(view => view
                .WithModelOfType<CreateCardViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
        {
            this.OneTimeSetup();
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
            this.TearDownBase();
        }

        [Fact]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.png");

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.PlayCards))
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
                       CardTypeId = 2,
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
                   .To<EventController>(c => c.All(1)));
            this.TearDownBase();
        }

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
        {
            this.OneTimeSetup();
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
            this.OneTimeSetup();
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
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<PlayCardController>
                        .Instance(instance => instance
                            .WithData(data.PlayCards.ToList()))
                        .Calling(c => c.All(1))
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<CardListViewModel>()
                            .Passing(model =>
                            {
                                Assert.Equal(19, model.Count);
                            }));

            this.TearDownBase();
        }

        [Fact]
        public void AllEventCardsShouldReturnDefaultViewWithCorrectModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<PlayCardController>
                        .Instance(instance => instance
                            .WithData(data.PlayCards.ToList()))
                        .Calling(c => c.AllEventCards(1))
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<CardListViewModel>()
                            .Passing(model =>
                            {
                                Assert.Equal(3, model.Count);
                            }));

            this.TearDownBase();
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var card = data.PlayCards.FirstOrDefault(x => x.Id == "c330fecf-61a9-4e03-8052-cd2b9583a251");
            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(data.PlayCards))
                .Calling(c => c.ById("c330fecf-61a9-4e03-8052-cd2b9583a251", "Ancestral-Recall"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleCardViewModel>());

            TearDownBase();
        }

        [Fact]
        public void AddCommentShouldSaveCommentAndSaveMessageAboutSuccessInTempAndShouldRedirectToAction()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.Users))
                .Calling(c => c.Comment(
                    new SingleCardViewModel
                    {
                        Name = "Ancestral Recall",
                        CommentTitle = "Ancestral Recall",
                        CommentDescription = "testTestTest",
                        CommentReview = "testestTestTestestTestTestestTestTest",
                        CommentSuggestions = "testTestTestestTestTestestTestTes",
                    }
                , "c330fecf-61a9-4e03-8052-cd2b9583a251"))
                .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Comment added successfully."))
               .AndAlso()
               .ShouldReturn()
               .RedirectToAction("ById", "PlayCard", new { id = "c330fecf-61a9-4e03-8052-cd2b9583a251", information = "Ancestral-Recall", Area = string.Empty });

            TearDownBase();
        }

        [Fact]
        public void AddCommentShouldReturnUnauthorizedIfUserIsNotInRole()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(MemberRoleName))
                    .WithData(data.Users))
                .Calling(c => c.Comment(
                    new SingleCardViewModel
                    {
                        Name = "Ancestral Recall",
                        CommentTitle = "Ancestral Recall",
                        CommentDescription = "testTestTest",
                        CommentReview = "testestTestTestestTestTestestTestTest",
                        CommentSuggestions = "testTestTestestTestTestestTestTes",
                    }
                , "c330fecf-61a9-4e03-8052-cd2b9583a251"))
               .ShouldReturn()
               .Unauthorized();

            TearDownBase();
        }

        [Fact]
        public void AddCommentWithInvalidModelShouldHaveInvalidModelStateAndShouldReturnView()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.Users))
                .Calling(c => c.Comment(
                    new SingleCardViewModel
                    { }
                , "c330fecf-61a9-4e03-8052-cd2b9583a251"))
              .ShouldHave()
               .InvalidModelState()
                .AndAlso()
               .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleCardViewModel>());
            TearDownBase();
        }

        [Fact]
        public void ApproveAlreadyApprovedCardShouldReturnBadRequest()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.ApproveCard("c330fecf-61a9-4e03-8052-cd2b9583a251"))
               .ShouldReturn()
              .BadRequest();
            TearDownBase();
        }

        [Fact]
        public void ApproveCardShouldRedirectToCorrectLocation()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.ApproveCard("5f3f96a8-836a-479c-93c8-6921feb79366"))
               .ShouldReturn()
               .RedirectToAction("ById", "Member", new { id = "2738e787-5d57-4bc7-b0d2-287242f04695", Area = "Administration" });
            TearDownBase();
        }

        [Fact]
        public void PromoteCardShouldReturnRedirectToAction()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Promote("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("ById", "PlayCard", new { id = "19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1", information = "Swamp", Area = string.Empty });

            TearDownBase();
        }

        [Fact]
        public void PromoteNonExistentCardShouldReturnBadRequest()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Promote("19b67a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .BadRequest();

            TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldRedirectToActionIfRequestIsMadeByAdmin()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("MyData", "User");

            TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldRedirectToActionIfRequestIsMadeByOwwwner()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("MyData", "User");

            TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldReturnUnAuthorizedIfRequestIsNotMadeByTheOwner()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .Unauthorized();

            TearDownBase();
        }

        [Fact]
        public void DeleteNonExistentCardShouldReturnBadRequest()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd2"))
               .ShouldReturn()
               .BadRequest();

            TearDownBase();
        }
    }
}

﻿namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Drawing;
    using System.Drawing.Imaging;
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
                 .WithData(data.PlayCards)
                .WithData(data.ManaCosts))
               .Calling(c => c.Add(
                   new CreateCardViewModel
                   {
                       Name = "TestTestTest",
                       CardRemoteUrl = "aaaaa",
                       CardDefaultDescription = "aaaa",
                       RedManaCost = 1,
                       BlackManaCost = 2,
                       BlueManaCost = 3,
                       ColorlessManaCost = 4,
                       WhiteManaCost = 5,
                       GreenManaCost = 6,
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
            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            MyController<PlayCardController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                 .WithData(data.PlayCards)
                 .WithData(data.ManaCosts))
               .Calling(c => c.Create(
                   new CreateCardViewModel
                   {
                       Name = "TestTestTest",
                       CardRemoteUrl = "aaaaa",
                       CardDefaultDescription = "aaaa",
                       CardFrameColorId = 1,
                       RedManaCost = 1,
                       BlackManaCost = 2,
                       BlueManaCost = 3,
                       ColorlessManaCost = 4,
                       WhiteManaCost = 5,
                       GreenManaCost = 6,
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
                   .To<EventController>(c => c.All(With.No<int>())));
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
            this.OneTimeSetup();
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

            this.TearDownBase();
        }

        [Fact]
        public void AddCommentShouldSaveCommentAndSaveMessageAboutSuccessInTempAndShouldRedirectToAction()
        {
            this.OneTimeSetup();
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
                    },
                "c330fecf-61a9-4e03-8052-cd2b9583a251"))
                .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Comment added successfully."))
               .AndAlso()
               .ShouldReturn()
               .RedirectToAction("ById", "PlayCard", new { id = "c330fecf-61a9-4e03-8052-cd2b9583a251", information = "Ancestral-Recall", Area = string.Empty });

            this.TearDownBase();
        }

        [Fact]
        public void AddCommentShouldReturnUnauthorizedIfUserIsNotInRole()
        {
            this.OneTimeSetup();
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
                    },
                "c330fecf-61a9-4e03-8052-cd2b9583a251"))
               .ShouldReturn()
               .Unauthorized();

            this.TearDownBase();
        }

        [Fact]
        public void AddCommentWithInvalidModelShouldHaveInvalidModelStateAndShouldReturnView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.Users))
                .Calling(c => c.Comment(
                    new SingleCardViewModel
                    { },
                "c330fecf-61a9-4e03-8052-cd2b9583a251"))
              .ShouldHave()
               .InvalidModelState()
                .AndAlso()
               .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleCardViewModel>());
            this.TearDownBase();
        }

        [Fact]
        public void ApproveAlreadyApprovedCardShouldReturnBadRequest()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.ApproveCard("c330fecf-61a9-4e03-8052-cd2b9583a251"))
               .ShouldReturn()
              .BadRequest();
            this.TearDownBase();
        }

        [Fact]
        public void ApproveCardShouldRedirectToCorrectLocation()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.ApproveCard("5f3f96a8-836a-479c-93c8-6921feb79366"))
               .ShouldReturn()
               .RedirectToAction("ById", "Member", new { id = "2738e787-5d57-4bc7-b0d2-287242f04695", Area = "Administration" });
            this.TearDownBase();
        }

        [Fact]
        public void PromoteCardShouldReturnRedirectToAction()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Promote("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("ById", "PlayCard", new { id = "19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1", information = "Swamp", Area = string.Empty });

            this.TearDownBase();
        }

        [Fact]
        public void PromoteNonExistentCardShouldReturnBadRequest()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Promote("19b67a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .BadRequest();

            this.TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldRedirectToActionIfRequestIsMadeByAdmin()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(AdministratorRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("MyData", "User");

            this.TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldRedirectToActionIfRequestIsMadeByOwwwner()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .RedirectToAction("MyData", "User");

            this.TearDownBase();
        }

        [Fact]
        public void DeleteCardShouldReturnUnAuthorizedIfRequestIsNotMadeByTheOwner()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1"))
               .ShouldReturn()
               .Unauthorized();

            this.TearDownBase();
        }

        [Fact]
        public void DeleteNonExistentCardShouldReturnBadRequest()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<PlayCardController>
               .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("f6f94be8-49e0-4a28-9e7f-797c40e7e169").InRole(MemberRoleName))
                    .WithData(data.PlayCards))
                .Calling(c => c.Delete("19b87a65-3352-4ee6-9c6a-5b7dfb82bfd2"))
               .ShouldReturn()
               .BadRequest();

            this.TearDownBase();
        }
    }
}

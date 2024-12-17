namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Event;
    using Xunit;

    public class EventControllerTest : UnitTestBase
    {
        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<EventController>
                        .Instance(instance => instance
                            .WithData(data.Events.ToList()))
                        .Calling(c => c.All(1))
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<EventListViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<EventController>
                .Instance(instance => instance
                    .WithData(data.Events))
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData("Event Title", "Create Event EventEventEventEventEventEventEvent")]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel(string title, string content)
        {
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

            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<EventController>
                .Instance(instance => instance
                    .WithUser(c => c.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7"))
                    .WithData(data.Users))
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
                   .To<EventController>(c => c.Create()));
        }
    }
}

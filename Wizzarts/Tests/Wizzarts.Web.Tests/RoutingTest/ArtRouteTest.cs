namespace Wizzarts.Web.Tests.RoutingTest
{
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Art;
    using Xunit;

    public class ArtRouteTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
                => MyRouting
        .Configuration()
                    .ShouldMap("/Art/All")
                    .To<ArtController>(c => c.All(With.No<int>()));

        [Fact]
        public void GetCreateShouldBeRoutedCorrectly()
            => MyRouting
        .Configuration()
                .ShouldMap("/Art/Add")
                .To<ArtController>(c => c.Add());

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Art/Edit/ab8532f9-2a2f-4b65-96f1-90e5468fbed2")
                .To<ArtController>(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"));

        [Theory]
        [InlineData("Test Article", "Test Article Content")]
        public void PostEditShouldBeRoutedCorrectly(string title, string content)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Image", "aaaa.jpg");

            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Art/Edit/ab8532f9-2a2f-4b65-96f1-90e5468fbed2")
                    .WithFormFields(new
                    {
                        Title = title,
                        Description = content,
                    }))
                .To<ArtController>(c => c.Edit(
                    new EditArtViewModel
                    {
                        Title = title,
                        Description = content,
                    },
                    "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"));
        }

        [Fact]
        public void GetByIdShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Art/ById/ab8532f9-2a2f-4b65-96f1-90e5468fbed2")
               .To<ArtController>(c => c.ById("ab8532f9-2a2f-4b65-96f1-90e5468fbed2", With.No<string>()));

        [Fact]
        public void GetByUserIdShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Art/ByUserId/1")
               .To<ArtController>(c => c.ByUserId(1));
    }
}

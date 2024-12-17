namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Article;
    using Xunit;

    using static Wizzarts.Common.GlobalConstants;

    public class ArticleControllerTest : UnitTestBase
    {
        [Fact]
        public void EditGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsers()
           => MyController<ArticleController>
               .Calling(c => c.Edit(With.Empty<int>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get));

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repositoryArticle, cache);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArticleViewModel
            {
                Title = "test",
                Description = "test",
                ShortDescription = "test",
                ImageUrl = file,
            };

            MyController<ArticleController>
                 .Instance(instance => instance
                   .WithData(data.Articles.FirstOrDefault(x => x.Id == 1)))
               .Calling(c => c.Edit(
                   1,
                   model))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Post));

            this.TearDownBase();
        }

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repositoryArticle, cache);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArticleViewModel
            {
                Title = "test",
                Description = "test",
                ShortDescription = "test",
                ImageUrl = file,
            };

            MyController<ArticleController>
                .Instance(instance => instance

                   .WithData(data.Articles.FirstOrDefault(x => x.Id == 1)))
                .Calling(c => c.Edit(1, With.Default<EditArticleViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<EditArticleViewModel>());

            this.TearDownBase();
        }

        [Fact]
        public void EditPostShouldRedirectToActionWhenValidModelState()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repositoryArticle, cache);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var model = new EditArticleViewModel
            {
                Title = "testtesttesttest",
                Description = "testtesttesttesttesttesttesttesttest",
                ShortDescription = "testtesttesttesttesttesttesttest",
                ImageUrl = file,
            };

            MyController<ArticleController>
                .Instance(instance => instance
                    .WithUser(x1 => x1.WithIdentifier("2b346dc6-5bd7-4e64-8396-15a064aa27a7").WithRoleType(AdministratorRoleName))
                   .WithData(data.Articles.FirstOrDefault(x => x.Id == 1)))
                .Calling(c => c.Edit(1, model))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticleController>(c => c.ById(1, "testtesttesttest")));

            this.TearDownBase();
        }

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
           => MyController<ArticleController>
               .Calling(c => c.Create())
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get))

               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<ArticleController>
                .Instance(instance => instance
                    .WithData(data.Articles)
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
                .Calling(c => c.Create(With.Empty<CreateArticleViewModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post));
            this.TearDownBase();
        }

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
        {

            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<ArticleController>
                .Instance(instance => instance
                    .WithData(data.Articles))
                .Calling(c => c.Create(With.Default<CreateArticleViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CreateArticleViewModel>());
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

            MyController<ArticleController>
                .Instance(instance => instance
                    .WithData(data.Arts)
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
               .Calling(c => c.Create(new CreateArticleViewModel
               {
                   Title = "testtesttest",
                   Description = "testtesttesttesttesttesttesttesttest",
                   ShortDescription = "testtesttesttesttesttesttesttest",
                   ImageUrl = file,
               }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Article added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                    .To<UserController>(c => c.MyData(With.No<int>())));
            this.TearDownBase();
        }
    }
}

namespace Wizzarts.Services.Data.Tests.ArticleServiceTest
{
    using System;
    using System.Drawing.Imaging;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Article;
    using Xunit;

    public class ArticleServiceTest : UnitTestBase
    {
        public ArticleServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task Article_Get_All_Should_Return_Correct_Art_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var articleService = new ArticleService(fileService, articleRepository, cache);
            var articles = articleService.GetAll<ArticleInListViewModel>(1, 6);
            int articleCount = articles.Count();
            Assert.Equal(5, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task Article_Get_All_Not_For_Main_Page_Should_Return_Correct_Art_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var articleService = new ArticleService(fileService, articleRepository, cache);
            var articles = await articleService.GetAllUserArticles<ArticleInListViewModel>();
            int articleCount = articles.Count();
            var testArticle = data.Articles.FirstOrDefault();
            Assert.Equal(1, articleCount);
            Assert.Equal("Call to arms",testArticle.Title);

            this.TearDownBase();
        }

        [Fact]
        public async Task Articles_Get_Count_Should_Return_Correct_Article_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var articleService = new ArticleService(fileService, articleRepository, cache);
            int articleCount = await articleService.GetCount();
            Assert.Equal(6, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task Article_Get_Random_Count_Should_Return_Correct_Article_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var articleService = new ArticleService(fileService, articleRepository, cache);
            var articles = articleService.GetRandom<ArticleInListViewModel>(3);
            int articleCount = articles.Count();
            Assert.Equal(3, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Article_Should_Change_The_Total_Count_Of_Articles_And_Add_The_Correct_Art()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                },
                userId,
                path,
                isPremium);
            var count = await repository.All().CountAsync();
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            Assert.Equal(7, count);
            Assert.Equal("New", testArticle.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Article_With_Wrong_File_Format_Should_Throw_An_Exception()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                },
                userId,
                path,
                isPremium));
            Assert.Equal("Invalid image", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task Update_Article_Should_Change_The_Correct_Article_Title()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);

            var testArticle = new EditArticleViewModel()
            {
                Title = "The newest Article",
            };

            await service.UpdateAsync(1, testArticle);
            var articleInDb = await data.Articles.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.Equal(testArticle.Title, articleInDb.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Article_GetBy_Id_Should_Return_Correct_Art()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);

            var article = await service.GetById<SingleArticleViewModel>(1);

            Assert.Equal("Wizzarts card game release announcement.", article.Title);

            this.TearDownBase();
        }

        [Fact]
        public async Task Article_Get_All_Art_By_UserId_Should_Return_The_Correct_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);

            var articlesByUserMetzen =
              await service.GetAllArticlesByUserId<ArticleInListViewModel>("2b346dc6-5bd7-4e64-8396-15a064aa27a7", 1, 4);
            Assert.Equal(4, articlesByUserMetzen.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Delete_Article_Should_Change_The_Total_Count_Of_Articles_And_Should_Remove_The_Correct_Item()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                },
                userId,
                path,
                isPremium);
            var count = await repository.All().CountAsync();
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            int testArticleId = testArticle.Id;
            string testArticleName = testArticle.Title;
            await service.DeleteAsync(testArticleId);
            Assert.Equal(7, testArticleId);
            Assert.Equal("New", testArticleName);
            Assert.Equal(6, repository.All().Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_Article_Should_Change_New_Article_Status_To_Approve_ByAdmin_And_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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

            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                },
                userId,
                path,
                isPremium);
            var count = await repository.All().CountAsync();
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            int testArticleId = testArticle.Id;
            string testArticleName = testArticle.Title;
            bool approvalStatusBefore = testArticle.ApprovedByAdmin;
            await service.ApproveArticle(testArticleId);
            Assert.Equal("New", testArticleName);
            Assert.Equal(7, count);
            Assert.False(approvalStatusBefore);
            Assert.True(testArticle.ApprovedByAdmin);
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_Approved_Article_Should_Return_Null()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;

            // not working with new  image verification
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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

            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, userId, path, isPremium);
            var count = await repository.All().CountAsync();
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            testArticle.ApprovedByAdmin = true;
            int testArticleId = testArticle.Id;
            Assert.Null(await service.ApproveArticle(testArticleId));
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_Non_Existing_Article_Should_Return_Null()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            Assert.Null(await service.ApproveArticle(10));
            this.TearDownBase();
        }

        [Fact]
        public async Task Article_Has_User_With_Id_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, userId, path, isPremium);
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            testArticle.ApprovedByAdmin = true;
            int testArticleId = testArticle.Id;
            Assert.True(await service.HasUserWithIdAsync(testArticleId, userId));
            this.TearDownBase();
        }

        [Fact]
        public async Task Checking_For_Existing_Article_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var fileService = new FileService();
            var service = new ArticleService(fileService, repository, cache);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";
            bool isPremium = false;
            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, userId, path, isPremium);
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            testArticle.ApprovedByAdmin = true;
            int testArticleId = testArticle.Id;
            Assert.True(await service.ArticleExist(testArticleId));
            this.TearDownBase();
        }
    }
}

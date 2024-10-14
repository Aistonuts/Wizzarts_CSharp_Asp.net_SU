namespace Wizzarts.Services.Data.Tests.ArticleServiceTest
{
    using System;
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
        public async Task ArticleGetAllShouldReturnCorrectArtCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var articleService = new ArticleService(articleRepository, cache);
            var articles = articleService.GetAll<ArticleInListViewModel>(1, 3);
            int articleCount = articles.Count();
            Assert.Equal(3, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task ArticlesGetCountShouldReturnCorrectArticleCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var articleService = new ArticleService(articleRepository, cache);
            int articleCount = articleService.GetCount();
            Assert.Equal(6, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task ArticleGetRandomCountShouldReturnCorrectArticleCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var articleRepository = new EfDeletableEntityRepository<Article>(data);
            var articleService = new ArticleService(articleRepository, cache);
            var articles = articleService.GetRandom<ArticleInListViewModel>(3);
            int articleCount = articles.Count();
            Assert.Equal(3, articleCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task CreateArticleShouldChangeTheTotalCountOfArticlesAndAddTheCorrectArt()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, UserId, path);
            var count = await repository.All().CountAsync();
            var testArticle = data.Articles.FirstOrDefault(x => x.Title == "New");
            Assert.Equal(7, count);
            Assert.Equal("New", testArticle.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task CreateArticleWithWrongFileFormatShouldThrowAnException()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, UserId, path));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task UpdateArticleShouldChangeTheCorrectArticleTitle()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);

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
        public async Task ArticleGetByIdShouldReturnCorrectArt()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);

            var article = service.GetById<SingleArticleViewModel>(1);

            Assert.Equal("Wizzarts card game release announcement.", article.Title);

            this.TearDownBase();
        }

        [Fact]
        public async Task ArticleGetAllArtByUserIdShouldReturnTheCorrectCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);

            var articlesByUserMetzen =
                service.GetAllArticlesByUserId<ArticleInListViewModel>("2b346dc6-5bd7-4e64-8396-15a064aa27a7", 1, 4);
            Assert.Equal(4, articlesByUserMetzen.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task DeleteArticleShouldChangeTheTotalCountOfArticlesAndShouldRemoveTheCorrectItem()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, UserId, path);
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
        public async Task ApproveArticleShouldChangeNewArticleStatusToApprovedByAdminToTrue()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            await service.CreateAsync(
                new CreateArticleViewModel
                {
                    Title = "New",
                    ImageUrl = file,
                    Description = "Test",
                    ShortDescription = "Test",
                }, UserId, path);
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
    }
}

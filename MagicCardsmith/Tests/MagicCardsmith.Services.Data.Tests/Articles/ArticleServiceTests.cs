namespace MagicCardsmith.Services.Data.Tests.Articles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Data.Repositories;
    using MagicCardsmith.Services.Data.Tests.Common;
    using MagicCardsmith.Services.Data.Tests.Mocks;
    using MagicCardsmith.Services.Data.Tests.UnitTests;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;

    [TestFixture]
    public class ArticleServiceTests : UnitTestsBase
    {

       
        public ArticleServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            
        }


        [Test]
        public void GetAllShouldReturnTheCorrectNumberOfArticles()
        {
            var repository = new Mock<IDeletableEntityRepository<Article>>();
            var cache = new MemoryCache(new MemoryCacheOptions());
            repository.Setup(r => r.AllAsNoTracking()).Returns(new List<Article>
                                                    {
                                                        new Article {Id = 1},
                                                        new Article { Id = 2},
                                                        new Article {Id = 3},
                                                    }.AsQueryable());
            var service = new ArticleService(repository.Object, cache);

            var allArticles = service.GetAll<IndexPageArticleViewModel>(1, 3);
            Assert.That(allArticles.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task CreateArticleShouldAddTheCorrectArticles()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            await data.Articles.AddAsync(new Article { Id = 1, Title = "New Article" });
            await data.Articles.AddAsync(new Article { Id = 2, Title = "Second New Article" });
            await data.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId ="2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\MagicCardsmith\\Web\\MagicCardsmith.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            await service.CreateAsync(
                new CreateArticleInputModel
            {
                Title = "New",
                ImageUrl = file,
            }, UserId, path);
            var count = await repository.All().CountAsync();
            Assert.That(repository.All().Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdaterticleShouldEditTheCorrectArticleAndChangeItsTitleToNewOne()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            data.Articles.Add(new Article { Id = 1, Title = "New Article" });
            data.Articles.Add(new Article { Id = 2, Title = "Second New Article" });
            await data.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);
            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\MagicCardsmith"));
            string path = baseDirectory + "\\Web\\MagicCardsmith.Web\\wwwroot" + "/images";
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            var model = new EditArticleInputModel
            {
                Id = 1,
                Title = "Edited Article",
            };
            await service.UpdateAsync(1, model);
            var testedArticle = data.Articles.FirstOrDefault(x => x.Id == 1);
            Assert.That(testedArticle.Title, Is.EqualTo("Edited Article"));
        }

        [Test]
        public void GetRandomNumberOfArticlesShouldReturnTheCorrectCountInTheRightOrder()
        {
            var repository = new Mock<IDeletableEntityRepository<Article>>();
            var cache = new MemoryCache(new MemoryCacheOptions());
            repository.Setup(r => r.All()).Returns(new List<Article>
                                                    {
                                                        new Article {Id = 1},
                                                        new Article { Id = 2},
                                                        new Article {Id = 3},
                                                    }.AsQueryable());
            var service = new ArticleService(repository.Object, cache);

            var allArticles = service.GetRandom<IndexPageArticleViewModel>(3);
            var firstArticleId = allArticles.FirstOrDefault();
            var lastArticleId  = allArticles.ToList().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.That(allArticles.Count(), Is.EqualTo(3));
            Assert.That(firstArticleId.Id, Is.EqualTo(1));
            Assert.That(lastArticleId.Id, Is.EqualTo(3));
        }


        [Test]
        public async Task GetByIdArticleShouldReturnTheCorrectArticle()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            data.Articles.Add(new Article { Id = 1, Title = "New Article" });
            data.Articles.Add(new Article { Id = 2, Title = "Second New Article" });
            await data.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repository, cache);

            var article = new SingleArticleViewModel();
            article = service.GetById<SingleArticleViewModel>(2);
            Assert.That(article.Title, Is.EqualTo("Second New Article"));
        }
    }
}

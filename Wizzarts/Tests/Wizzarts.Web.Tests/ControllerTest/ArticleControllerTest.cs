using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Data;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class ArticleControllerTest : UnitTestBase
    {


        [Fact]
        public void EditGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsers()
           => MyController<ArticleController>
               .Calling(c => c.Edit())
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get));

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repositoryArticle, cache);
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

            TearDownBase();
        }
        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repositoryArticle, cache);
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

                   .WithData( data.Articles.FirstOrDefault(x=>x.Id == 1)))
                .Calling(c => c.Edit(1, With.Default<EditArticleViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<EditArticleViewModel>());

            TearDownBase();
        }

        [Fact]
        public void EditPostShouldRedirectToActionWhenValidModelState()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArticle = new EfDeletableEntityRepository<Article>(data);
            var service = new ArticleService(repositoryArticle, cache);
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

                   .WithData(data.Articles.FirstOrDefault(x => x.Id == 1)))
                .Calling(c => c.Edit(1, model))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticleController>(c => c.ById(1)));

            TearDownBase();

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
           => MyController<ArticleController>
               .Calling(c => c.Create(With.Empty<CreateArticleViewModel>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<ArticleController>
                .Calling(c => c.Create(With.Default<CreateArticleViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<CreateArticleViewModel>());

        [Fact]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.jpg");

            MyController<ArticleController>
                .Instance()
                .WithUser()
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
                   .To<HomeController>(c => c.Index()));
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;

            MyController<ArticleController>
             .Instance(instance => instance
                 .WithUser()
                 .WithData(data.Articles.FirstOrDefault(x => x.Id == 1)))
             .Calling(c => c.ById(1))
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<SingleArticleViewModel>());

            TearDownBase();
        }
    }
}

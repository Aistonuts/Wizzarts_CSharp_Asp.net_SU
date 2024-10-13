using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Wizzarts.Common;
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
    public class ArtControllerTest : UnitTestBase
    {
        [Fact]
        public void AddShouldReturnViewWithCorrectModel()
        {
            MyController<ArtController>
            .Calling(c => c.Add(With.Default<AddArtViewModel>())) // Provides a global service.
            .ShouldReturn()
            .View(With.Default<AddArtViewModel>());
        }

        [Fact]
        public void AddArtShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<ArtController>
                .Calling(c => c.Add(With.Default<AddArtViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<AddArtViewModel>());

        [Theory]
        [InlineData("Art Title", "Art Content")]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel(string title, string content)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");



            MyController<ArtController>
                .Instance()
                .WithUser()
               .Calling(c => c.Add(new AddArtViewModel
               {
                   Title = title,
                   Description = content,
                   RemoteImageUrl = "test",
                   Image = file,
               }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .Data(data => data
               .WithSet<Art>(set =>
               {
                   set.ShouldNotBeEmpty();
                   set.SingleOrDefault(a => a.Title == title).ShouldNotBeNull();
               }))
               .AndAlso()
               .ShouldHave()
               .TempData(temp => temp.ContainingEntryWithValue("Art added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<UserController>(c => c.MyData(With.No<int>())));
        }

        [Fact]
        public void EditGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsers()
           => MyController<ArtController>
               .Calling(c => c.Edit(With.Any<string>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get));



        [Fact]
        public void EditGetShouldReturnNotFoundWhenInvalidId()
           => MyMvc.Controller<ArtController>()
            
               .Calling(c => c.Edit(With.Any<string>()))
                
               .ShouldReturn()
               .NotFound();


        [Fact]
        public void EditGetShouldReturnViewWithCorrectModelWhenCorrectUser()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);

            MyController<ArtController>
               .Instance(instance => instance
                   .WithData(data.Arts.FirstOrDefault(x=>x.Id == "ab8532f9-2a2f-4b65-96f1-90e5468fbed2")))
               .Calling(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
               .ShouldReturn()
               .View(view => view
                   .WithModelOfType<EditArtViewModel>()
                   .Passing(article => article.Title == "Ancestral recall"));

            TearDownBase();
        }

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);

            var model = new EditArtViewModel
            {
                Title = "test",
                Description = "test",
            };


            MyController<ArtController>
                 .Instance(instance => instance
                   .WithData(data.Arts.FirstOrDefault(x => x.Id == "ab8532f9-2a2f-4b65-96f1-90e5468fbed2")))
               .Calling(c => c.Edit(
                   model,
                   "ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Post));

            TearDownBase();
        }
    }
}

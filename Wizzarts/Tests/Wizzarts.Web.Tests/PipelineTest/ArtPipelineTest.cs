﻿namespace Wizzarts.Web.Tests.PipelineTest
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Art;
    using Xunit;

    public class ArtPipelineTest : UnitTestBase
    {
        [Fact]
        public void GetAddShouldShouldReturnView()
           => MyMvc
               .Pipeline()
               .ShouldMap(request => request
               .WithLocation("/Art/Add")
                   .WithUser())
               .To<ArtController>(c => c.Add())
               .Which()
               .ShouldReturn()
               .View();

        [Theory]
        [InlineData("Test Article", "Test Article Content")]
        public void PostCreateShouldSaveArticleHaveValidModelStateAndRedirect(string title, string content)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            MyMvc
               .Pipeline()
               .ShouldMap(request => request
                   .WithMethod(HttpMethod.Post)
                   .WithLocation("/Art/Add")
                   .WithFormFields(new
                   {
                       Title = title,
                       Description = content,
                   })
                   .WithUser()
                   .WithAntiForgeryToken())
               .To<ArtController>(c => c.Add(new AddArtViewModel
               {
                   Title = title,
                   Description = content,
               }))
               .Which()
               .ShouldHave()
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
                .View();
        }

        [Fact]
        public void GetEditShouldShouldReturnNotFound()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyMvc
               .Pipeline()
               .ShouldMap(request => request
                   .WithLocation("/Art/Edit/ab8532f9-2a2f-4b65-96f1-90e5468fbed2")
                   .WithUser(X => X.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695")))
               .To<ArtController>(c => c.Edit("ab8532f9-2a2f-4b65-96f1-90e5468fbed2"))
               .Which(c => c.WithData(this.dbContext.Arts.ToList()))
               .ShouldReturn()
                .View();

            this.TearDownBase();
        }

        [Theory]
        [InlineData(1, 19)]
        public void GetAllWithPageShouldReturnDefaultViewWithCorrectModel(int page, int expectedCount)
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyMvc
                .Pipeline()
                .ShouldMap($"/Art/All/1")
                .To<ArtController>(c => c.All(page))
                .Which(controller => controller
                    .WithData(this.dbContext.Arts.ToList()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArtListViewModel>()
                    .Passing(articleListing =>
                    {
                        articleListing.Arts.Count().ShouldBe(expectedCount);
                    }));
            this.TearDownBase();
        }
    }
}

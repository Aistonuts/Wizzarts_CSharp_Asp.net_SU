using Microsoft.AspNetCore.Http;
using MyTested.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Store;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class StoreControllerTest : UnitTestBase
    {
        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
           => MyController<StoreController>
               .Calling(c => c.Create())
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Get))

               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
          => MyController<StoreController>
              .Calling(c => c.Create(With.Empty<CreateStoreViewModel>()))
              .ShouldHave()
              .ActionAttributes(attrs => attrs
                  .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<StoreController>
                .Calling(c => c.Create(With.Default<CreateStoreViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<CreateStoreViewModel>());

        [Fact]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 1, bytes.Length, "Data", "dummy.jpg");

            MyController<StoreController>
                .Instance()
                .WithUser()
               .Calling(c => c.Create(new CreateStoreViewModel
               {
                   StoreName = "test",
                   StoreOwnerId = "testtesttest",
                   StoreAddress = "testtesttesttest",
                   StoreCity = "testtesttesttest",
                   StoreCountry = "testtesttesttest",
                   StorePhoneNumber = "00000100000",
                   StoreImage = file,
               }))
               .ShouldHave()
               .ValidModelState()
               .AndAlso()
               .ShouldHave()
               .TempData(tempData => tempData
                   .ContainingEntryWithValue("Store added successfully."))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<StoreController>(c => c.All(With.No<int>())));
        }

        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            MyController<StoreController>
                        .Instance(instance => instance
                            .WithData(data.Stores.ToList()))
                        .Calling(c => c.All(1))
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<StoreListViewModel>());

            TearDownBase();
        }
    }
}

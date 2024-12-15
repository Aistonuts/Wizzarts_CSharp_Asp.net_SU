namespace Wizzarts.Services.Data.Tests.StoreServiceTest
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
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Store;
    using Xunit;

    public class StoreServiceTest : UnitTestBase
    {
        public StoreServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateStoreShouldChangeTheTotalCountOfStoresInDbAndShouldAddTheCorrectStore()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

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

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };

            await service.CreateAsync(newStore, userId, path);

            var count = await repository.All().CountAsync();
            var testStore = data.Stores.FirstOrDefault(x => x.Name == "test");

            Assert.Equal(7, count);
            Assert.Equal(newStore.StoreName, testStore.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task CreateStoreWithWrongFileTypeShouldThrowAnException()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

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
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.nft")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(newStore, userId, path));

            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllShouldReturnCorrectStoreCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);
            var stores = await service.GetAll<StoreInListViewModel>(1, 5);
            int storeCount = stores.Count();
            Assert.Equal(5, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllNoPaginationShouldReturnCorrectStoreCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);
            var stores = await service.GetAll<StoreInListViewModel>();
            int storeCount = stores.Count();
            Assert.Equal(6, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllByUserIdShouldReturnCorrectStoreCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);
            var stores = await service.GetAllStoresByUserId<StoreInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 2);
            int storeCount = stores.Count();
            Assert.Equal(2, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresCountShouldReturnCorrectStoreCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);
            int storeCount = await service.GetCount();

            Assert.Equal(6, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveStoreShouldChangeNewStoreApprovalStatusToApproved()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

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

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };

            await service.CreateAsync(newStore, userId, path);

            var count = await repository.All().CountAsync();
            var testStore = data.Stores.FirstOrDefault(x => x.Name == "test");
            var storeApprovedStatusBefore = testStore.ApprovedByAdmin;
            await service.ApproveStore(testStore.Id);
            Assert.Equal(7, count);
            Assert.Equal(newStore.StoreName, testStore.Name);
            Assert.False(storeApprovedStatusBefore);
            Assert.True(testStore.ApprovedByAdmin);
            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllApprovedStoresByUserIdShouldReturnCorrectStoreCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var fileService = new FileService();
            var service = new StoreService(repository, fileService);
            var stores = await service.GetAllApprovedStoresByUserId<StoreInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            int storeCount = stores.Count();
            Assert.Equal(2, storeCount);

            this.TearDownBase();
        }
    }
}

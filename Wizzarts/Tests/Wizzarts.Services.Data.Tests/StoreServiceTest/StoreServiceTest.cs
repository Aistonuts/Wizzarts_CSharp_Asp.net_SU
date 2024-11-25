using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Store;
using Xunit;

namespace Wizzarts.Services.Data.Tests.StoreServiceTest
{
    public class StoreServiceTest : UnitTestBase
    {
        public StoreServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateStoreShouldChangeTheTotalCountOfStoresInDbAndShouldAddTheCorrectStore()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);

            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreOwnerId = "testtesttest",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };

            await service.CreateAsync(newStore, UserId, path);

            var count = await repository.All().CountAsync();
            var testStore = data.Stores.FirstOrDefault(x => x.Name == "test");

            Assert.Equal(7, count);
            Assert.Equal(newStore.StoreName, testStore.Name);
            TearDownBase();
        }

        [Fact]
        public async Task CreateStoreWithWrongFileTypeShouldThrowAnException()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);

            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreOwnerId = "testtesttest",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(newStore, UserId, path));

            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
            TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllShouldReturnCorrectStoreCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);
            var stores = await service.GetAll<StoreInListViewModel>(1, 5);
            int storeCount = stores.Count();
            Assert.Equal(5, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllNoPaginationShouldReturnCorrectStoreCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);
            var stores = await service.GetAll<StoreInListViewModel>();
            int storeCount = stores.Count();
            Assert.Equal(6, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllByUserIdShouldReturnCorrectStoreCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);
            var stores = await service.GetAllStoresByUserId<StoreInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695",1,2);
            int storeCount = stores.Count();
            Assert.Equal(2, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task StoresCountShouldReturnCorrectStoreCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);
            int storeCount = await service.GetCount();

            Assert.Equal(6, storeCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveStoreShouldChangeNewStoreApprovalStatusToApproved()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);

            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var newStore = new CreateStoreViewModel
            {
                StoreName = "test",
                StoreOwnerId = "testtesttest",
                StoreAddress = "testtesttesttest",
                StoreCity = "testtesttesttest",
                StoreCountry = "testtesttesttest",
                StorePhoneNumber = "00000100000",
                StoreImage = file,
            };

            await service.CreateAsync(newStore, UserId, path);

            var count = await repository.All().CountAsync();
            var testStore = data.Stores.FirstOrDefault(x => x.Name == "test");
            var storeApprovedStatusBefore = testStore.ApprovedByAdmin;
            await service.ApproveStore(testStore.Id);
            Assert.Equal(7, count);
            Assert.Equal(newStore.StoreName, testStore.Name);
            Assert.False(storeApprovedStatusBefore);
            Assert.True(testStore.ApprovedByAdmin);
            TearDownBase();
        }

        [Fact]
        public async Task StoresGetAllApprovedStoresByUserIdShouldReturnCorrectStoreCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repository = new EfDeletableEntityRepository<Store>(data);
            var service = new StoreService(repository);
            var stores = await service.GetAllApprovedStoresByUserId<StoreInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            int storeCount = stores.Count();
            Assert.Equal(2, storeCount);

            this.TearDownBase();
        }
    }
}

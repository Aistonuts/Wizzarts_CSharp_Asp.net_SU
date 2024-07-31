using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Xunit;
using Xunit.Sdk;

namespace Wizzarts.Services.Data.Tests.ArtServiceTest
{
    public class ArtServiceTest : UnitTestBase
    {
        public ArtServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }


        [Fact]
        public async Task ArtIsBase64StringWorksAsIntended()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            string base64 = "ZXhhbXBsZQ==";
            string notBase64 = "ZXhhbXBsZQ==11111123";
            bool isBase64 = artService.IsBase64String(base64);
            bool isNotBase64 = artService.IsBase64String(notBase64);
            Assert.True(isBase64);
            Assert.False(isNotBase64);
            this.TearDownBase();
        }

        [Fact]
        public async Task ArtGetByIdShouldReturnCorrectArt()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            var art = artService.GetById<SingleArtViewModel>("ab8532f9-2a2f-4b65-96f1-90e5468fbed2");

            Assert.Equal("Ancestral recall", art.Title);

            this.TearDownBase();
        }

        [Fact]
        public async Task ArtGetAllShouldReturnCorrectArtCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            var art = artService.GetAll<ArtInListViewModel>(1,19);
            int artCount = art.Count();
            Assert.Equal(19, artCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task ArtGetCountShouldReturnCorrectArtCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            int artCount = artService.GetCount();
            Assert.Equal(19, artCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task CreateArtShouldChangeTheTotalCountOfArts()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var service = new ArtService(repository, cache);

            string UserId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\MagicCardsmith.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };

            await service.AddAsync(testArtPiece, UserId, path);

            var count = await repository.All().CountAsync();
            Assert.Equal(20, count);
            this.TearDownBase();
        }

        [Fact]
        public async Task CreateArtWithWrongFileFormatShouldThrowInvalidImageExtensionError()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var service = new ArtService(repository, cache);

            string UserId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.AddAsync(testArtPiece, UserId, path));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task CreateArtShouldAddTheCorrectArt()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var service = new ArtService(repository, cache);

            string UserId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };

            await service.AddAsync(testArtPiece, UserId, path);

            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");
            Assert.Equal(testArtPiece.Title, testArt.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task UpdateArtShouldChangeTheCorrectArtPieceTitle()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var service = new ArtService(repository, cache);

            var testArtPiece = new EditArtViewModel()
            {
                Title = "The newest ArtPiece",
            };

            await service.UpdateAsync(testArtPiece, "ab8532f9-2a2f-4b65-96f1-90e5468fbed2");
            var artPiece = await data.Arts.FirstOrDefaultAsync(x => x.Id == "ab8532f9-2a2f-4b65-96f1-90e5468fbed2");

            Assert.Equal(testArtPiece.Title, artPiece.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task ArtDeleteShouldLowerTheArtsCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            await artService.DeleteAsync("ab8532f9-2a2f-4b65-96f1-90e5468fbed2");
            int artCount = artService.GetCount();
            var artPiece = await data.Arts.FirstOrDefaultAsync(x => x.Id == "ab8532f9-2a2f-4b65-96f1-90e5468fbed2");
            Assert.Equal(18, artCount);
            Assert.Null(artPiece);
            this.TearDownBase();
        }

        [Fact]
        public async Task ArtAllArtByUserIdShouldReturnTheCorrectCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            var artByUserDrawgoon = artService.GetAllArtByUserId<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 8);
            Assert.Equal(8, artByUserDrawgoon.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task ArtAllArtByUserIdPaginationLessShouldReturnTheCorrectCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var artService = new ArtService(repositoryArt, cache);
            var artByUserDrawgoon = artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            Assert.Equal(8, artByUserDrawgoon.Count());
            this.TearDownBase();
        }
    }
}

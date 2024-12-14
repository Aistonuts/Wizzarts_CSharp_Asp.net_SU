namespace Wizzarts.Services.Data.Tests.ArtServiceTest
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
    using NuGet.Protocol.Core.Types;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Art;
    using Xunit;

    public class ArtServiceTest : UnitTestBase
    {
        public ArtServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task Art_Is_Base_64String_Works_As_Intended()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            string base64 = "ZXhhbXBsZQ==";
            string notBase64 = "ZXhhbXBsZQ==11111123";
            bool isBase64 = artService.IsBase64String(base64);
            bool isNotBase64 = artService.IsBase64String(notBase64);
            Assert.True(isBase64);
            Assert.False(isNotBase64);
            this.TearDownBase();
        }

        [Fact]
        public async Task Art_Get_By_Id_Should_Return_CorrectArt()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            var art = await artService.GetById<SingleArtViewModel>("ab8532f9-2a2f-4b65-96f1-90e5468fbed2");

            Assert.Equal("Ancestral recall", art.Title);

            this.TearDownBase();
        }

        [Fact]
        public async Task Art_Get_All_Should_Return_Correct_Art_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            var art = artService.GetAll<ArtInListViewModel>(1, 19);
            int artCount = art.Count();
            Assert.Equal(19, artCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task Art_Get_Count_Should_Return_Correct_Art_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            int artCount = await artService.GetCountAsync();
            Assert.Equal(19, artCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Art_Should_Change_The_Total_Count_Of_Arts()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);

            var count = await repository.All().CountAsync();
            Assert.Equal(20, count);
            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Art_With_Wrong_File_Format_Should_Throw_Invalid_Image_Extension_Error()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");
            bool isPremium = false;

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.AddAsync(testArtPiece, userId, path, isPremium));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Art_Should_Add_The_Correct_Art()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);

            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");
            Assert.Equal(testArtPiece.Title, testArt.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Update_Art_Should_Change_The_Correct_Art_Piece_Title()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

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
        public async Task Art_Delete_Should_Lower_The_Arts_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            await artService.DeleteAsync("ab8532f9-2a2f-4b65-96f1-90e5468fbed2");
            int artCount = await artService.GetCountAsync();
            var artPiece = await data.Arts.FirstOrDefaultAsync(x => x.Id == "ab8532f9-2a2f-4b65-96f1-90e5468fbed2");
            Assert.Equal(18, artCount);
            Assert.Null(artPiece);
            this.TearDownBase();
        }

        [Fact]
        public async Task Art_AllArtByUserId_Should_Return_The_Correct_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            var artByUserDrawgoon = await artService.GetAllArtByUserId<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 8);
            Assert.Equal(8, artByUserDrawgoon.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Art_AllArtByUserId_PaginationLess_Should_Return_The_Correct_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var artService = new ArtService(repositoryArt, cache, fileService);
            var artByUserDrawgoon = await artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            Assert.Equal(8, artByUserDrawgoon.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Art_Exist_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);

            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");

            Assert.Equal(testArtPiece.Title, testArt.Title);
            Assert.True(await service.ArtExist(testArt.Id));
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_New_Added_Art_Should_Change_Its_Status_To_Approved()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);
            var count = await repository.All().CountAsync();
            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");
            bool approvalStatusBefore = testArt.ApprovedByAdmin;
            await service.ApproveArt(testArt.Id);
            Assert.Equal(20, count);
            Assert.Equal(testArtPiece.Title, testArt.Title);
            Assert.False(approvalStatusBefore);
            Assert.True(testArt.ApprovedByAdmin);
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_Approved_Art_Should_Return_Null()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);
            var count = await repository.All().CountAsync();
            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");
            testArt.ApprovedByAdmin = true;
            Assert.Null(await service.ApproveArt(testArt.Id));
            ;
            this.TearDownBase();
        }

        [Fact]
        public async Task Approve_Non_Existing_Art_Should_Return_Null()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            Assert.Null(await service.ApproveArt("111-b-1"));
            this.TearDownBase();
        }

        [Fact]
        public async Task Has_User_With_Id_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var fileService = new FileService();
            var service = new ArtService(repository, cache, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new AddArtViewModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };
            bool isPremium = false;
            await service.AddAsync(testArtPiece, userId, path, isPremium);

            var testArt = data.Arts.FirstOrDefault(x => x.Title == "The newest ArtPiece");
            bool hasUserWithId = await service.HasUserWithIdAsync(testArt.Id, "66030199-349f-4e35-846d-97685187a565");
            Assert.Equal(testArtPiece.Title, testArt.Title);
            Assert.True(hasUserWithId);

            this.TearDownBase();
        }
    }
}

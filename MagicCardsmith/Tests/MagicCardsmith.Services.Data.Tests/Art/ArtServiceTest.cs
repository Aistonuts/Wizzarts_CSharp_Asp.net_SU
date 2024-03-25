
namespace MagicCardsmith.Services.Data.Tests.Art
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Data.Repositories;
    using MagicCardsmith.Services.Data.Tests.UnitTests;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Art;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ArtServiceTest : UnitTestsBase
    {
        public ArtServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Test]
        public async Task CreateArtShouldAddTheCorrectArt()
        {
            OneTimeSetup();
            var data = this.dbContext;

            await data.Arts.AddAsync(new Art { Id = "66030199-349f-4e35-846d-97685187a565", Title = "New Fancy piece" });
            await data.Arts.AddAsync(new Art { Id = "7519476e-b5a0-464b-bcb9-e6f2aa7ed73c",Title = "Second Fancy piece" });
            await data.Arts.AddAsync(new Art { Id = "2ba62b79-65ed-45da-bfeb-a5c50b4f8a2a", Title = "First Fancy piece" });
            await data.SaveChangesAsync();

           

            using var repository = new EfDeletableEntityRepository<Art>(data);
            var service = new ArtService(repository);

            int UserId = 1;
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\MagicCardsmith\\Web\\MagicCardsmith.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testArtPiece = new CreateArtInputModel()
            {
                Title = "The newest ArtPiece",
                Image = file,
            };

            await service.CreateAsync(testArtPiece, UserId, path);

            var count = await repository.All().CountAsync();
            Assert.That(count, Is.EqualTo(4));
            
            
        }
    }
}

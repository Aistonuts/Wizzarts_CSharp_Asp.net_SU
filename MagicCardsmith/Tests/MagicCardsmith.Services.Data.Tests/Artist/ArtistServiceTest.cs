using MagicCardsmith.Data.Models;
using MagicCardsmith.Data.Repositories;
using MagicCardsmith.Services.Data;
using MagicCardsmith.Web.ViewModels.Article;

namespace MagicCardsmith.Services.Data.Tests.Artist
{
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data.Tests.UnitTests;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Premium;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    [TestFixture]
    public class ArtistServiceTest : UnitTestsBase
    {
        public ArtistServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

        }


        [Test]
        public async Task CreateArtistShouldAddDataInTheDatabaseWithCorrectProperties()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            await data.Artists.AddAsync(new Artist { Id = 1, Nickname = "Test1", UserId = "66030199-349f-4e35-846d-97685187a565" });
            await data.Artists.AddAsync(new Artist { Id = 2, Nickname = "Test2", UserId = "7519476e-b5a0-464b-bcb9-e6f2aa7ed73c" });
            await data.Artists.AddAsync(new Artist { Id = 3, Nickname = "Test3", UserId = "2ba62b79-65ed-45da-bfeb-a5c50b4f8a2a" });
            await data.Users.AddAsync(new ApplicationUser { Id = "f3993d55-2bf6-49fe-b944-552bc892e5be", Nickname = "UserTest", });
            var artPiece = new Art()
            {
                Id = "84642140-622f-41d9-9dd0-e4dbac356294",
                Title = "Fancy Art",
                ApplicationUserId = "f3993d55-2bf6-49fe-b944-552bc892e5be",
            };
            await data.Arts.AddAsync(artPiece);
            await data.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Artist>(data);
            var service = new ArtistService(data, repository);
            var testArtist = new BecomeArtistViewModel()
            {
                Id = 4,
                Nickname = "Test4",
                Email = "test@test.com",
                AvatarUrl = "www.w.com",
            };
            await service.Create("5d43dc90-f92f-4730-9096-3a2bc1e9b7d2", testArtist);
            var count = await repository.All().CountAsync();
            var testedArticle = data.Artists.FirstOrDefault(x => x.Id == 4);
            var exist = await service.ArtistExistsByUserIdAsync("5d43dc90-f92f-4730-9096-3a2bc1e9b7d2");
            var artistId = await service.GetArtistIdByUserIdAsync("5d43dc90-f92f-4730-9096-3a2bc1e9b7d2");
            var artistById = service.GetById<SingleArtistViewModel>(4);

            var randomArtists = service.GetRandom<ArtistInListViewModel>(4);

            var firstRandomArtist = randomArtists.FirstOrDefault();


            var hasArt = await service.HasArtByUserIdAsync("f3993d55-2bf6-49fe-b944-552bc892e5be");

            var artistExist = await service.ArtistExistsByUserIdAsync("66030199-349f-4e35-846d-97685187a565");

            Assert.That(repository.All().Count(), Is.EqualTo(4));
            Assert.That(testedArticle.Nickname, Is.EqualTo("Test4"));
            Assert.That(exist, Is.True);
            Assert.That(artistId, Is.EqualTo("4"));
            Assert.That(artistById.Nickname, Is.EqualTo("Test4"));
            Assert.That(service.GetCount(), Is.EqualTo(4));
            Assert.That(randomArtists.Count(), Is.EqualTo(4));
            Assert.That(hasArt, Is.True);
            Assert.That(artistExist, Is.True);
        }



    }
}
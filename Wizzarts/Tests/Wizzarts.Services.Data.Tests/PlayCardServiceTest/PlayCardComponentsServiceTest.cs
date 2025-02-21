namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Xunit;

    public class PlayCardComponentsServiceTest : UnitTestBase
    {
        public PlayCardComponentsServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task PlayCardGetAllMethodForGettingVariousComponentsWorksAsIntendedAndReturnCorretCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardManaCostRepository = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardComponentsService(
                cardManaRepository,
                cardManaCostRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cardGameExpansionRepository);
            var cardFrameColor = await service.GetAllCardFrames();
            var cardType = await service.GetAllCardType();
            var cardGameExpansion = await service.GetAllExpansionInListView();
            Assert.Equal(7, cardType.Count());
            Assert.Equal(3, cardGameExpansion.Count());
            Assert.Equal(6, cardFrameColor.Count());
            this.TearDownBase();
        }
    }
}

namespace Wizzarts.Services.Data.Tests.SearchServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Xunit;

    public class SearchServiceTest : UnitTestBase
    {
        public SearchServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetACardByTermShouldReturnCorrectCardAndCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            var service = new SearchService(data);

            var cards = await service.GetAllCardsByTerm("Mox Sapphire");
            var card = cards.FirstOrDefault(x => x.CardName == "Mox Sapphire");
            Assert.Equal(1, cards.Count());
            Assert.Equal("Mox Sapphire", card.CardName);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCardsByTermShouldReturnCorrectCardAndCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            var service = new SearchService(data);

            var cards = await service.GetAllCardsByTerm("Mox");
            var card = cards.FirstOrDefault(x => x.CardName == "Mox Sapphire");
            Assert.Equal(5, cards.Count());
            Assert.Equal("Mox Sapphire", card.CardName);
            this.TearDownBase();
        }
    }
}

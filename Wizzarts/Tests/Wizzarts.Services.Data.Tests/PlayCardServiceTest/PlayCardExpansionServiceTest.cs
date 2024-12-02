namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Expansion;
    using Xunit;

    public class PlayCardExpansionServiceTest : UnitTestBase
    {
        public PlayCardExpansionServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task PlayCardExpansionGetAllWithCardsInExpansionsShouldReturnCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardExpansionService(cardGameExpansionRepository);

            var cardGameExpansion = await service.GetAll<ExpansionInListViewModel>();

            Assert.Equal(2, cardGameExpansion.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardExpansionGetByIdShouldReturnCorrentExpansionTitle()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardExpansionService(cardGameExpansionRepository);

            var cardGameExpansion = await service.GetById<ExpansionInListViewModel>(1);

            Assert.Equal("Alpha", cardGameExpansion.Title);
            this.TearDownBase();
        }
    }
}

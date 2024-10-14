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

namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
    public class PlayCardExpansionServiceTest : UnitTestBase
    {
        public PlayCardExpansionServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task PlayCardExpansionGetAllWithCardsInExpansionsShouldReturnCorrectCount()
        {
            OneTimeSetup();
            var data = this.dbContext;
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardExpansionService(cardGameExpansionRepository);

            var cardGameExpansion = service.GetAll<ExpansionInListViewModel>();

            Assert.Equal(2, cardGameExpansion.Count());
            TearDownBase();
        }

        [Fact]
        public async Task PlayCardExpansionGetByIdShouldReturnCorrentExpansionTitle()
        {
            OneTimeSetup();
            var data = this.dbContext;
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardExpansionService(cardGameExpansionRepository);

            var cardGameExpansion = service.GetById<ExpansionInListViewModel>(1);

            Assert.Equal("Alpha", cardGameExpansion.Title);
            TearDownBase();
        }
    }
}

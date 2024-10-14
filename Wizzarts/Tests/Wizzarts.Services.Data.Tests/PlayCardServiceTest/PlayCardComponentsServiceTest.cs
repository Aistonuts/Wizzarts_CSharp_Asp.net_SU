using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;
using Xunit;

namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
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
            using var blackManaRepository = new EfDeletableEntityRepository<BlackMana>(data);
            using var blueManaRepository = new EfDeletableEntityRepository<BlueMana>(data);
            using var redManaRepository = new EfDeletableEntityRepository<RedMana>(data);
            using var whiteManaRepository = new EfDeletableEntityRepository<WhiteMana>(data);
            using var greenManaRepository = new EfDeletableEntityRepository<GreenMana>(data);
            using var colorlessManaRepository = new EfDeletableEntityRepository<ColorlessMana>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);
            var service = new PlayCardComponentsService(
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cardGameExpansionRepository
                );
            var redCardMana = service.GetAllRedMana();
            var blueCardMana = service.GetAllBlueMana();
            var greenCardMana = service.GetAllGreenMana();
            var blackCardMana = service.GetAllBlackMana();
            var whiteCardMana = service.GetAllWhiteMana();
            var cardFrameColor = service.GetAllCardFrames();
            var colorlessCardMana = service.GetAllColorlessMana();
            var cardType = service.GetAllCardType();
            var cardGameExpansion = service.GetAllExpansionInListView();
            Assert.Equal(6, redCardMana.Count());
            Assert.Equal(6, blueCardMana.Count());
            Assert.Equal(6, greenCardMana.Count());
            Assert.Equal(6, whiteCardMana.Count());
            Assert.Equal(6, blackCardMana.Count());
            Assert.Equal(10, colorlessCardMana.Count());
            Assert.Equal(7, cardType.Count());
            Assert.Equal(3, cardGameExpansion.Count());
            Assert.Equal(6, cardFrameColor.Count());
            this.TearDownBase();
        }
    }
}

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
using System.Xml.Linq;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;
using Xunit;

namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
    public class PlayCardServicesTest : UnitTestBase
    {
        public PlayCardServicesTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateNewCardShouldChangeTheTotalCountOfCardsInTheDb()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            string UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            await service.CreateAsync(
                new CreateCardViewModel
                {
                    Name = "TestTestTest",
                    BlackManaId = 1,
                    BlueManaId = 1,
                    RedManaId = 1,
                    WhiteManaId = 1,
                    GreenManaId = 2,
                    ColorlessManaId = 1,
                    CardFrameColorId = 1,
                    CardTypeId = 1,
                    AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                    Power = "1",
                    Toughness = "2",
                    ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                }, UserId, 1, path, true, true, "captured");

            var currentCount = await playCardRepository.All().CountAsync();
            var testPlayCard = data.PlayCards.FirstOrDefault(x=> x.Name == "TestTestTest");

            Assert.Equal(20, currentCount);
            Assert.Equal("TestTestTest", testPlayCard.Name);

            TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllShouldReturnCorrectPlayCardsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            var playCards = service.GetAll<CardInListViewModel>(1, 19);
            int playCardCount = playCards.Count();
            Assert.Equal(19,playCardCount);

            TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetRandomShouldReturnCorrectPlayCardsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            var playCards = service.GetRandom<CardInListViewModel>(5);
            int playCardCount = playCards.Count();
            Assert.Equal(5, playCardCount);
            TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetByIdShouldReturnCorrectCardName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            var playCard = service.GetById<SingleCardViewModel>("5f3f96a8-836a-479c-93c8-6921feb79366");

            Assert.Equal("Mox Sapphire", playCard.Name);
            TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllManByCardIdShouldReturnCorrectCardManaCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            var playCardMana = service.GetAllCardManaByCardId<ManaListViewModel>("7e1ef124-3c7f-4318-89b3-18315d7eaf81");

            // colorless x1, redmana x2//
            Assert.Equal(3, playCardMana.Count());
            TearDownBase();
        }


        [Fact]
        public async Task PlayCardGetAllPlayCardsByUserIdShouldReturnCorrectCountOfPlayCardsAndTheFirstShouldHavTheCorrectId()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
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

            var service = new PlayCardService(
                playCardRepository,
                cardManaRepository,
                blackManaRepository,
                blueManaRepository,
                redManaRepository,
                whiteManaRepository,
                greenManaRepository,
                colorlessManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache);

            var playCardsByDrawgoon = service.GetAllCardsByUserId<CardInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695",1,19);
            var specificCard = playCardsByDrawgoon.FirstOrDefault(x => x.Name == "Mox Sapphire");

            int playCardCount = playCardsByDrawgoon.Count();
            Assert.Equal(19, playCardCount);
            Assert.Equal("5f3f96a8-836a-479c-93c8-6921feb79366", specificCard.Id);
            TearDownBase();
        }
    }
}
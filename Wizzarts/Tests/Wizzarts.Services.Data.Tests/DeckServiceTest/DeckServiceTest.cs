using System.IO;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Deck;
using Wizzarts.Web.ViewModels.PlayCard;
using Xunit;

namespace Wizzarts.Services.Data.Tests.DeckServiceTest
{
    public class DeckServiceTest : UnitTestBase
    {
        public DeckServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateDeckShouldAddNewDeckWithCorrectName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);
            var deckOfCards = data.CardDecks.FirstOrDefault(x => x.Name == "Test");

            Assert.Equal("Test", deckOfCards.Name);
            Assert.Equal(2, data.CardDecks.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllDecksShouldReturnCorrectCountOfDecksWithCorrectNames()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);
            await deckService.LockDeck(1);

            var deckOfCards = deckService.GetAll<DeckInListViewModel>();
            var currentDeck = deckOfCards.FirstOrDefault(x => x.Name == "Test");
            Assert.Equal("Test", currentDeck.Name);
            Assert.Equal(1, deckOfCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllDecksByUserShouldReturnCorrectCountOfDecksWithCorrectNames()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);
            await deckService.LockDeck(1);

            var deckOfCards = deckService.GetAllDecksByUserId<DeckInListViewModel>(userId);
            var currentDeck = deckOfCards.FirstOrDefault(x => x.Name == "Test");
            Assert.Equal("Test", currentDeck.Name);
            Assert.Equal(2, deckOfCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetDecksByIdShouldReturnCorrectCountOfDecksWithCorrectNames()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);
            await deckService.LockDeck(1);

            var deckOfCards = await deckService.GetById<DeckInListViewModel>(1);
            Assert.Equal("Test", deckOfCards.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task AddCardToNewDeckShouldAddCorrectCardAndIncreaseCountOfCards()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);


            await deckService.AddAsync(1, "c330fecf-61a9-4e03-8052-cd2b9583a251");
            var deckWithCards = data.DeckOfCards.FirstOrDefault(x => x.DeckId == 1);
            Assert.Equal("c330fecf-61a9-4e03-8052-cd2b9583a251", deckWithCards.PlayCardId);
            Assert.Equal(1, data.DeckOfCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCardsInDeckIdShouldAddCorrectCardAndIncreaseCountOfCards()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);


            await deckService.AddAsync(1, "c330fecf-61a9-4e03-8052-cd2b9583a251");
            var deckWithCards = deckService.GetAllCardsInDeckId<CardInListViewModel>(1);

            var firstCard = deckWithCards.FirstOrDefault(x => x.Id == "c330fecf-61a9-4e03-8052-cd2b9583a251");
            Assert.Equal("Ancestral Recall", firstCard.Name);
            Assert.Equal(1, deckWithCards.Count());
            this.TearDownBase();
        }

        //[Fact]
        //public async Task RemoveAsyncShouldRemoveCorrectCardAndDecreaseCountOfCards()
        //{
        //    this.OneTimeSetup();
        //    var data = this.dbContext;
        //    var cache = new MemoryCache(new MemoryCacheOptions());

        //    using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
        //    using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
        //    using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
        //    using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
        //    using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
        //    using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
        //    using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
        //    using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
        //    using var blackManaRepository = new EfDeletableEntityRepository<BlackMana>(data);
        //    using var blueManaRepository = new EfDeletableEntityRepository<BlueMana>(data);
        //    using var redManaRepository = new EfDeletableEntityRepository<RedMana>(data);
        //    using var whiteManaRepository = new EfDeletableEntityRepository<WhiteMana>(data);
        //    using var greenManaRepository = new EfDeletableEntityRepository<GreenMana>(data);
        //    using var colorlessManaRepository = new EfDeletableEntityRepository<ColorlessMana>(data);
        //    using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
        //    using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
        //    using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

        //    var playCardService = new PlayCardService(
        //        playCardRepository,
        //        cardManaRepository,
        //        blackManaRepository,
        //        blueManaRepository,
        //        redManaRepository,
        //        whiteManaRepository,
        //        greenManaRepository,
        //        colorlessManaRepository,
        //        cardFrameColorRepository,
        //        cardTypeRepository,
        //        cache);
        //    var artService = new ArtService(repositoryArt, cache);
        //    var deckService = new DeckService(repositoryDeck, repositoryEvent, playCardRepository,
        //        repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
        //    string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
        //    string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
        //                  "/images";

        //    var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
        //    IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

        //    var deck = new CreateDeckViewModel()
        //    {
        //        Name = "Test",
        //        Description = "Test",
        //        ShippingAddress = "Test",
        //        StoreId = 1,
        //        Image = file,
        //        EventId = 4,
        //    };

        //    await deckService.CreateAsync(deck, userId, path);


        //    await deckService.AddAsync(1, "c330fecf-61a9-4e03-8052-cd2b9583a251");
        //    await deckService.AddAsync(1, "f43639ef-5503-4e8a-a75d-5651c645a03d");
        //    var currentCount = data.DeckOfCards.Count();
        //    await deckService.RemoveAsync(1, "c330fecf-61a9-4e03-8052-cd2b9583a251");
        //    var deckWithCards = deckService.GetAllCardsInDeckId<CardInListViewModel>(1);
        //    var firstCard = deckWithCards.FirstOrDefault(x => x.Id == "f43639ef-5503-4e8a-a75d-5651c645a03d");
        //    var newCount = data.DeckOfCards.Count();
        //    Assert.Equal("Bad Moon", firstCard.Name);
        //    Assert.Equal(2, currentCount);
        //    Assert.Equal(1, newCount);
        //    this.TearDownBase();
        //}

        [Fact]
        public async Task HasEventCardsShouldReturnCorrectCountOfCardsAndTheirName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);


            await deckService.AddAsync(1, "c330fecf-61a9-4e03-8052-cd2b9583a251");
            var currentCount = data.DeckOfCards.Count();
            var newDeck = deckService.HasEventCards(1);
            Assert.Equal(1, currentCount);
            Assert.True(newDeck);
            this.TearDownBase();
        }

        [Fact]
        public async Task LockDeckShouldLockDeckAdnReturnTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };

            await deckService.CreateAsync(deck, userId, path);
            var newDeck = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var currentDeckStatus = newDeck.IsLocked;
            await deckService.LockDeck(1);
            var newDeckStatus = newDeck.IsLocked;
            Assert.False(currentDeckStatus);
            Assert.True(newDeckStatus);
            this.TearDownBase();
        }


        [Fact]
        public async Task ChangeStatusAsyncShouldChangeDeckCurrentStatus()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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

            var playCardService = new PlayCardService(
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
            var artService = new ArtService(repositoryArt, cache);
            var deckService = new DeckService(repositoryDeck,repositoryOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" +
                          "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var deck = new CreateDeckViewModel()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StoreId = 1,
                Image = file,
                EventId = 4,
            };
            var currentDeck = new SingleDeckViewModel()
            {
                Id = 1,
                DeckStatusId = 3,
            };

            await deckService.CreateAsync(deck, userId, path);
            var currentStatus = data.CardDecks.FirstOrDefault(x => x.Id == 1).StatusId;
            await deckService.ChangeStatusAsync(currentDeck);
            var newStatus = data.CardDecks.FirstOrDefault(x => x.Id == 1).StatusId;
            Assert.Equal(1,currentStatus);
            Assert.Equal(3,newStatus);
            this.TearDownBase();
        } 
    }
}

namespace Wizzarts.Services.Data.Tests.DeckServiceTest
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
    using Moq;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Xunit;

    public class DeckServiceTest : UnitTestBase
    {
        public DeckServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task Create_Deck_Should_Add_New_Deck_With_Correct_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
            Assert.Equal(5, data.CardDecks.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Locked_Decks_Should_Return_Correct_Count_Of_Decks_With_Correct_Names()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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

            var deckOfCards = await deckService.GetAll<DeckInListViewModel>();
            var currentDeck = deckOfCards.FirstOrDefault(x => x.Name == "Test");
            Assert.Equal("Test", currentDeck.Name);
            Assert.Equal(3, deckOfCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Decks_By_User_Should_Return_Correct_Count_Of_Decks_With_Correct_Names()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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

            var deckOfCards = await deckService.GetAllDecksByUserId<DeckInListViewModel>(userId);
            var currentDeck = deckOfCards.FirstOrDefault(x => x.Name == "Test");
            Assert.Equal("Test", currentDeck.Name);
            Assert.Equal(4, deckOfCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Decks_By_Id_Should_Return_Correct_Count_Of_Decks_With_Correct_Names()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
        public async Task Add_Card_To_New_Deck_Should_Add_Correct_Card_And_Increase_Count_Of_Cards()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
        public async Task Add_Cards_In_Deck_Should_Add_Correct_Card_And_Increase_Count_Of_Cards()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
            var deckWithCards = await deckService.GetAllCardsInDeckId<CardInListViewModel>(1);

            var firstCard = deckWithCards.FirstOrDefault(x => x.Id == "c330fecf-61a9-4e03-8052-cd2b9583a251");
            Assert.Equal("Ancestral Recall", firstCard.Name);
            Assert.Equal(1, deckWithCards.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Has_Event_Cards_Should_Return_Correct_Count_Of_Cards_And_Their_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
            var newDeck = await deckService.HasEventCards(1);
            Assert.Equal(1, currentCount);
            Assert.True(newDeck);
            this.TearDownBase();
        }

        [Fact]
        public async Task Lock_Dec_kShould_Lock_Deck_And_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
        public async Task Change_Status_Async_Should_Change_Deck_Current_Status()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
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
            Assert.Equal(1, currentStatus);
            Assert.Equal(3, newStatus);
            this.TearDownBase();
        }

        [Fact]
        public async Task Has_Open_Deck_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";

            Assert.True(await deckService.HasOpenDecks(userId));
            this.TearDownBase();
        }

        [Fact]
        public async Task Checking_User_For_Closed_Decks_Should_Return_False()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
            string userId = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281";

            Assert.False(await deckService.HasOpenDecks(userId));
            this.TearDownBase();
        }

        [Fact]
        public async Task Locking_Deck_ShouldChangeItsStatusAndAlsoShouldRevertChangesAndUnlockAndSwitchStatusBetweenReadyAndOpen()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);
            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";

            var testLockedDeck = data.CardDecks.FirstOrDefault(x => x.Id == 4);

            var currentLockStatus = testLockedDeck.IsLocked;
            var currentReadyStatus = testLockedDeck.StatusId;

            await deckService.LockDeck(4);
            var newLockStatus = testLockedDeck.IsLocked;
            var newReadyStatus = testLockedDeck.StatusId;

            var testUnlockedDeck = data.CardDecks.FirstOrDefault(x => x.Id == 1);

            var currentLockStatusTwo = testUnlockedDeck.IsLocked;
            var currentReadyStatusTwo = testUnlockedDeck.StatusId;

            await deckService.LockDeck(1);
            var newLockStatusTwo = testUnlockedDeck.IsLocked;
            var newReadyStatusTwo = testUnlockedDeck.StatusId;

            Assert.True(currentLockStatus);
            Assert.Equal(3, currentReadyStatus);

            Assert.False(newLockStatus);
            Assert.Equal(1, newReadyStatus);

            Assert.False(currentLockStatusTwo);
            Assert.Equal(1, currentReadyStatusTwo);

            Assert.True(newLockStatusTwo);
            Assert.Equal(2, newReadyStatusTwo);
            this.TearDownBase();
        }

        // [Fact]
        // public async Task GetAllDeckStatusesShouldReturnCorrectCorrectStatusCountAndValue()
        // {
        //    this.OneTimeSetup();
        //    var data = this.dbContext;
        //    var cache = new MemoryCache(new MemoryCacheOptions());

        // using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
        //    using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
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
        //    using var cardOrderRepository = new EfDeletableEntityRepository<CardOrder>(data);

        // var playCardService = new PlayCardService(
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
        //    var deckService = new DeckService(repositoryDeck, repositoryOrder, cardOrderRepository, repositoryEvent, playCardRepository,
        //        repositoryDeckOfCards, repositoryStatus, repositoryUser, playCardService, artService);

        // var statuses = deckService.GetAllDeckStatuses().OrderByDescending(x => x.Id);

        // var firstDeckStatus = statuses.FirstOrDefault(x => x.Name == "Open");
        //    var secondDeckStatus = statuses.FirstOrDefault(x => x.Name == "Ready");

        // Assert.Equal(1,firstDeckStatus.Id);
        //    Assert.Equal(2, secondDeckStatus.Id);
        //    Assert.Equal(4, statuses.Count());
        //    this.TearDownBase();
        // }
        [Fact]
        public async Task Check_If_Deck_I_sLocked_Should_Return_True_If_Locked_And_False_If_Unlocked()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            Assert.True(await deckService.IsLocked(4));
            Assert.False(await deckService.IsLocked(1));
            this.TearDownBase();
        }

        [Fact]
        public async Task Order_Deck_Of_Cards_Should_Create_a_new_Order_And_Fill_The_Order_With_Cards_From_Deck()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var deckOrder = data.CardOrders.FirstOrDefault(x => x.OrderId == 1);

            Assert.Equal(cardId, deckOrder.PlayCardId);
            this.TearDownBase();
        }

        [Fact]
        public async Task Removing_Card_From_Card_Deck_Should_Remove_The_Card_And_Decrease_The_Count_Of_Cards_In_Deck()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            var firstCardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            var secondCardId = "f43639ef-5503-4e8a-a75d-5651c645a03d";
            await deckService.AddAsync(1, firstCardId);
            await deckService.AddAsync(1, secondCardId);
            var currentCountOfCardsInDeck = data.DeckOfCards.Count();
            await deckService.RemoveAsync(1, firstCardId);
            var newCountOfCardsInDeck = data.DeckOfCards.Count();

            var currentCard = data.DeckOfCards.FirstOrDefault(x => x.PlayCardId == secondCardId && x.DeckId == 1);

            Assert.Equal(2, currentCountOfCardsInDeck);

            Assert.Equal(1, newCountOfCardsInDeck);
            Assert.Equal(secondCardId, currentCard.PlayCardId);
            this.TearDownBase();
        }

        [Fact]
        public async Task Update_Deck_Should_Change_Existing_Deck_Shipping_Address()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            var testDeck = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testTitle = testDeck.Name;
            var testDescription = testDeck.Description;
            var testLocation = testDeck.StoreId;

            var deck = new EditDeckViewModel()
            {
                Id = 1,
                Name = "Changed",
                Description = "Changed",
                StoreId = 2,
            };
            await deckService.UpdateAsync(deck, 1);

            var testDeckUpdate = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testTitleUpdate = testDeckUpdate.Name;
            var testDescriptionUpdate = testDeckUpdate.Description;
            var testLocationUpdate = testDeckUpdate.StoreId;

            Assert.Equal(1, testLocation);
            Assert.Equal("Test", testTitle);
            Assert.Equal("Test", testDescription);
            Assert.Equal(2, testLocationUpdate);
            Assert.Equal("Changed", testTitleUpdate);
            Assert.Equal("Changed", testDescriptionUpdate);
            this.TearDownBase();
        }

        [Fact]
        public async Task Update_Deck_Should_Update_The_Existing_Deck()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            var testDeck = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testTitle = testDeck.Name;
            var testDescription = testDeck.Description;
            var testLocation = testDeck.StoreId;

            var deck = new EditDeckViewModel()
            {
                Id = 1,
                Name = "Changed",
                Description = "Changed",
                StoreId = 2,
            };
            await deckService.UpdateAsync(deck, 1);

            var testDeckUpdate = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testTitleUpdate = testDeckUpdate.Name;
            var testDescriptionUpdate = testDeckUpdate.Description;
            var testLocationUpdate = testDeckUpdate.StoreId;

            Assert.Equal(1, testLocation);
            Assert.Equal("Test", testTitle);
            Assert.Equal("Test", testDescription);
            Assert.Equal(2, testLocationUpdate);
            Assert.Equal("Changed", testTitleUpdate);
            Assert.Equal("Changed", testDescriptionUpdate);
            this.TearDownBase();
        }

        [Fact]
        public async Task Update_Deck_Should_Change_Existing_Deck_Shipping_Location()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            using var repositoryOrder = new EfDeletableEntityRepository<Order>(data);
            using var repositoryDeck = new EfDeletableEntityRepository<CardDeck>(data);
            using var repositoryDeckOfCards = new EfDeletableEntityRepository<DeckOfCards>(data);
            using var repositoryCardsInOrder = new EfDeletableEntityRepository<CardOrder>(data);
            using var repositoryEvent = new EfDeletableEntityRepository<Event>(data);
            using var repositoryUser = new EfDeletableEntityRepository<ApplicationUser>(data);
            using var repositoryArt = new EfDeletableEntityRepository<Art>(data);
            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryStatus = new EfDeletableEntityRepository<DeckStatus>(data);
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
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventParticipantRepository = new EfDeletableEntityRepository<EventParticipant>(data);

            var cardService = new PlayCardService(
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

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache);
            var eventService = new EventService(repositoryEvent, eventParticipantRepository, eventComponentsRepository);
            var deckService = new DeckService(repositoryDeck, repositoryOrder, repositoryCardsInOrder, repositoryEvent, playCardRepository,
                repositoryDeckOfCards, repositoryStatus, repositoryUser, cardService, artService, eventService);

            var testDeck = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testLocation = testDeck.StoreId;

            var deck = new SingleDeckViewModel()
            {
                Id = 1,
                StoreId = 2,
            };
            await deckService.UpdateShippingAsync(deck);

            var testDeckUpdate = data.CardDecks.FirstOrDefault(x => x.Id == 1);
            var testLocationUpdate = testDeckUpdate.StoreId;

            Assert.Equal(1, testLocation);
            Assert.Equal(2, testLocationUpdate);
            this.TearDownBase();
        }
    }
}

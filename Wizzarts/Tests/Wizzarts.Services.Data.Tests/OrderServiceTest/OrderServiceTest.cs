namespace Wizzarts.Services.Data.Tests.OrderServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Order;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Xunit;

    public class OrderServiceTest : UnitTestBase
    {
        public OrderServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CancelOrderShouldRemoveCardsFromOrderAndDeleteOrder()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var deckOrder = data.CardOrders.FirstOrDefault(x => x.OrderId == 1);

            var playCardId = deckOrder.PlayCardId;
            var cardsInOrderCount = data.CardOrders.Count();
            var ordersCount = data.Orders.Count();
            await orderService.CancelOrder(1);

            var cardsInOrderCountAfterCancel = data.CardOrders.Count();
            var ordersCountAfterCancel = data.Orders.Count();
            Assert.Equal(playCardId, deckOrder.PlayCardId);
            Assert.Equal(playCardId, deckOrder.PlayCardId);
            Assert.Equal(1, cardsInOrderCount);
            Assert.Equal(1, ordersCount);
            Assert.Equal(0, cardsInOrderCountAfterCancel);
            Assert.Equal(0, ordersCountAfterCancel);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllOrdersShouldReturnCorrectCount()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var deckOrder = await orderService.GetAll<BaseOrderViewModel>();

            Assert.Single(deckOrder);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCardsInOrderShouldReturnCorrectCountAndCorrectData()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);
            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var cardsInOrder = await orderService.GetAllCardsInOrderId<CardInListViewModel>(1);

            var card = cardsInOrder.FirstOrDefault();

            Assert.Single(cardsInOrder);
            Assert.Equal(cardId, card.Id);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllOrdersByUserShouldReturnCorrectCount()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var deckOrder = await orderService.GetAllOrdersByUserId<BaseOrderViewModel>("2b346dc6-5bd7-4e64-8396-15a064aa27a7");

            Assert.Single(deckOrder);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetOrderByIdShouldReturnCorrectOrderWithTitleDescriptionAndId()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var deckOrder = await orderService.GetById<BaseOrderViewModel>(1);
            var orderTitle = deckOrder.Title;
            var orderDeckId = deckOrder.DeckId;
            var orderDescription = deckOrder.Description;
            Assert.NotNull(deckOrder);
            Assert.Equal("Test", orderTitle);
            Assert.Equal(1, orderDeckId);
            Assert.Equal("Test", orderDescription);
            this.TearDownBase();
        }

        [Fact]
        public async Task HasUserWithIdShouldReturnTrue()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var hasCorrectUser = await orderService.HasUserWithIdAsync(1, userId);

            Assert.True(hasCorrectUser);
            this.TearDownBase();
        }

        [Fact]
        public async Task PauseOrderShouldChangeCurrentStatusToPending()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);
            var order = data.Orders.FirstOrDefault(x => x.Id == 1);
            order.OrderStatusId = 2;
            await data.SaveChangesAsync();
            await orderService.PauseOrder(1);

            Assert.Equal(1, order.OrderStatusId);
            this.TearDownBase();
        }

        [Fact]
        public async Task ReadyOrderShouldChangeCurrentStatusToShipping()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var order = data.Orders.FirstOrDefault(x => x.Id == 1);

            var currentStatus = order.OrderStatusId;
            await orderService.ReadyOrder(1);
            var newStatus = order.OrderStatusId;

            Assert.Equal(1, currentStatus);
            Assert.Equal(2, newStatus);
            this.TearDownBase();
        }

        [Fact]
        public async Task ShipOrderShouldChangeCurrentStatusToPendingShipped()
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
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);

            var fileService = new FileService();

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
                cache,
                fileService);

            var cardExpansionService = new PlayCardExpansionService(cardGameExpansionRepository);
            var artService = new ArtService(repositoryArt, cache, fileService);
            var deckService = new DeckService(
                repositoryDeck,
                repositoryOrder,
                repositoryCardsInOrder,
                repositoryEvent,
                playCardRepository,
                repositoryDeckOfCards,
                repositoryStatus,
                repositoryUser,
                cardService,
                artService,
                fileService);

            var orderService = new OrderService(repositoryOrder, repositoryCardsInOrder, cardService, cardExpansionService, playCardRepository);

            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";
            await deckService.AddAsync(1, cardId);

            var userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            await deckService.OrderAsync(1, userId);

            var order = data.Orders.FirstOrDefault(x => x.Id == 1);

            var currentStatus = order.OrderStatusId;
            await orderService.ShipOrder(1);
            var newStatus = order.OrderStatusId;

            Assert.Equal(1, currentStatus);
            Assert.Equal(3, newStatus);
            this.TearDownBase();
        }
    }
}

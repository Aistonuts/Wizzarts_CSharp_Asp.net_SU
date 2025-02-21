namespace Wizzarts.Services.Data.Tests.PlayCardTypeOfServiceTest
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;
    using Xunit;

    public class PlayCardServicesTest : UnitTestBase
    {
        public PlayCardServicesTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        // 19 cards in total, 19 owned by Drawgoon, 18 approved, 1 is not approved
        [Fact]
        public async Task CreateNewCardShouldChangeTheTotalCountOfCardsInTheDb()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            await service.CreateAsync(
                new CreateCardViewModel
                {
                    Name = "TestTestTest",
                    CardFrameColorId = 1,
                    CardTypeId = 1,
                    AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                    Power = "1",
                    Toughness = "2",
                    ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                }, userId, 1, path, 1, "captured");

            var currentCount = await playCardRepository.All().CountAsync();
            var testPlayCard = data.PlayCards.FirstOrDefault(x => x.Name == "TestTestTest");

            Assert.Equal(20, currentCount);
            Assert.Equal("TestTestTest", testPlayCard.Name);

            this.TearDownBase();
        }

        [Fact]
        public async Task AddNewCardShouldChangeTheTotalCountOfCardsInTheDb()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            await service.AddAsync(
                new CreateCardViewModel
                {
                    Name = "TestTestTest",
                    CardFrameColorId = 1,
                    CardTypeId = 1,
                    AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                    Power = "1",
                    Toughness = "2",
                    ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                }, userId, path, true, "captured");

            var currentCount = await playCardRepository.All().CountAsync();
            var testPlayCard = data.PlayCards.FirstOrDefault(x => x.Name == "TestTestTest");

            Assert.Equal(20, currentCount);
            Assert.Equal("TestTestTest", testPlayCard.Name);

            this.TearDownBase();
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectCardCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var currentCount = await playCardRepository.All().CountAsync();

            Assert.Equal(19, await service.GetCount());

            this.TearDownBase();
        }

        [Fact]
        public async Task PromoteANewlyAddedCardShouldChangeTheExpansion()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            string userId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            await service.AddAsync(
                new CreateCardViewModel
                {
                    Name = "TestTestTest",
                    CardFrameColorId = 1,
                    CardTypeId = 1,
                    AbilitiesAndFlavor = "Test Test Test Test Test Test  TestTestTestTestTestTestTest",
                    Power = "1",
                    Toughness = "2",
                    ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                    GameExpansionId = 3,
                }, userId, path, true, "captured");

            var testPlayCard = data.PlayCards.FirstOrDefault(x => x.Name == "TestTestTest");
            var currentExpansion = testPlayCard.CardGameExpansion.Title;

            await service.Promote(testPlayCard.Id);
            var newExpansion = testPlayCard.CardGameExpansion.Title;

            Assert.Equal("Beta", currentExpansion);
            Assert.Equal("Second", newExpansion);

            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllShouldReturnCorrectPlayCardsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCards = service.GetAll<CardInListViewModel>(1, 18);
            int playCardCount = playCards.Count();
            Assert.Equal(18, playCardCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetRandomShouldReturnCorrectPlayCardsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCards = service.GetRandom<CardInListViewModel>(5);
            int playCardCount = playCards.Count();
            Assert.Equal(5, playCardCount);
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetByIdShouldReturnCorrectCardName()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCard = await service.GetById<SingleCardViewModel>("5f3f96a8-836a-479c-93c8-6921feb79366");

            Assert.Equal("Mox Sapphire", playCard.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllManByCardIdShouldReturnCorrectCardManaCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCardMana = await service.GetAllCardManaByCardId<ManaListViewModel>("7e1ef124-3c7f-4318-89b3-18315d7eaf81");

            // colorless x1, redmana x2//
            Assert.Equal(3, playCardMana.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllPlayCardsByUserIdShouldReturnCorrectCountOfPlayCardsAndTheFirstShouldHavTheCorrectId()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCardsByDrawgoon = await service.GetAllCardsByUserId<CardInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 19);
            var specificCard = playCardsByDrawgoon.FirstOrDefault(x => x.Name == "Mox Sapphire");

            int playCardCount = playCardsByDrawgoon.Count();
            Assert.Equal(19, playCardCount);
            Assert.Equal("5f3f96a8-836a-479c-93c8-6921feb79366", specificCard.Id);
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllCardsByCriteriaShouldReturnCorrectPlayCardsC()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var searchInputOne = new SingleDeckViewModel
            {
                SearchEvent = "Event",
                SearchName = "Ancestral Recall",
                SearchType = "Instant",
                SearchTypeId = 5,
            };
            var searchInputTwo = new SingleDeckViewModel
            {
                SearchEvent = "Event",
                SearchName = null,
                SearchType = "Instant",
                SearchTypeId = 5,
            };

            var searchInputThree = new SingleDeckViewModel
            {
                SearchEvent = "Event",
                SearchName = "Ancestral Recall",
                SearchType = null,
                SearchTypeId = 5,
            };

            var searchInputFour = new SingleDeckViewModel
            {
                SearchEvent = "Event",
                SearchName = null,
                SearchType = null,
            };

            var searchInputOneOne = new SingleDeckViewModel
            {
                SearchEvent = "Base",
                SearchName = "Crusade",
                SearchType = "Enchantment",
                SearchTypeId = 2,
            };
            var searchInputTwoTwo = new SingleDeckViewModel
            {
                SearchEvent = "Base",
                SearchName = null,
                SearchType = "Instant",
                SearchTypeId = 5,
            };

            var searchInputThreeThree = new SingleDeckViewModel
            {
                SearchEvent = "Base",
                SearchName = "Crusade",
                SearchType = null,
                SearchTypeId = 1,
            };

            var searchInputFourFour = new SingleDeckViewModel
            {
                SearchEvent = "Base",
                SearchName = null,
                SearchType = null,
            };

            // empty
            var searchInputEmpty = new SingleDeckViewModel
            {
            };

            // TestOne
            var resultTestOne = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputOne);
            var cardNameTestOne = resultTestOne.FirstOrDefault(x => x.Name == "Ancestral Recall");
            int countTestOne = resultTestOne.Count();

            // TestTwo
            var resultTestTwo = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputTwo);
            var firstCardNameTestTwo = resultTestTwo.FirstOrDefault(x => x.Name == "Ancestral Recall");
            var secondCardNameTestTwo = resultTestTwo.FirstOrDefault(x => x.Name == "Dark Ritual");
            int countTestTwo = resultTestTwo.Count();

            // TestThree
            var resultTestThree = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputThree);
            var firstCardNameTestThree = resultTestThree.FirstOrDefault(x => x.Name == "Ancestral Recall");

            int countTestThree = resultTestThree.Count();

            // TestFour
            var resultTestFour = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputFour);
            var firstCardNameTestFour = resultTestFour.FirstOrDefault(x => x.Name == "Ancestral Recall");
            var secondCardNameTestFour = resultTestFour.FirstOrDefault(x => x.Name == "Dark Ritual");
            var thirdCardNameTestFour = resultTestFour.FirstOrDefault(x => x.Name == "Bad Moon");
            int countTestFour = resultTestFour.Count();

            // TestOneOne
            var resultTestOneOne = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputOneOne);
            var cardNameTestOneOne = resultTestOneOne.FirstOrDefault(x => x.Name == "Crusade");
            int countTestOneOne = resultTestOneOne.Count();

            // TestTwoTwo
            var resultTestTwoTwo = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputTwoTwo);
            var firstCardNameTestTwoTwo = resultTestTwoTwo.FirstOrDefault(x => x.Name == "Gianth Growth");
            var secondCardNameTestTwoTwo = resultTestTwoTwo.FirstOrDefault(x => x.Name == "Healing Salve");
            var thirdCardNameTestTwoTwo = resultTestTwoTwo.FirstOrDefault(x => x.Name == "Lightning Bolt");
            int countTestTwoTwo = resultTestTwoTwo.Count();

            // TestThreeThree
            var resultTestThreeThree = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputThreeThree);
            var firstCardNameTestThreeThree = resultTestThreeThree.FirstOrDefault(x => x.Name == "Crusade");

            int countTestThreeThree = resultTestThreeThree.Count();

            // TestFourFour
            var resultTestFourFour = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputFourFour);
            var firstCardNameTestFourFour = resultTestFourFour.FirstOrDefault(x => x.Name == "Crusade");
            var secondCardNameTestFourFour = resultTestFourFour.FirstOrDefault(x => x.Name == "Dark Ritual");
            var thirdCardNameTestFourFour = resultTestFourFour.FirstOrDefault(x => x.Name == "Bad Moon");
            var fourthCardNameTestFourFour = resultTestFourFour.FirstOrDefault(x => x.Name == "Mox Ruby");
            var fifthCardNameTestFourFour = resultTestFourFour.FirstOrDefault(x => x.Name == "Black Lotus");
            int countTestFourFour = resultTestFourFour.Count();

            // TestEmptyInput
            var resultEmptyInput = await service.GetAllCardsByCriteria<CardInListViewModel>(searchInputEmpty);

            int countTestEmpty = resultEmptyInput.Count();

            // TestOne
            Assert.Equal(1, countTestOne);
            Assert.Equal("Ancestral Recall", cardNameTestOne.Name);

            // TestTwo
            Assert.Equal(2, countTestTwo);
            Assert.Equal("Ancestral Recall", firstCardNameTestTwo.Name);
            Assert.Equal("Dark Ritual", secondCardNameTestTwo.Name);

            // TestThree
            Assert.Equal(1, countTestThree);
            Assert.Equal("Ancestral Recall", firstCardNameTestThree.Name);

            // TestFour
            Assert.Equal(3, countTestFour);
            Assert.Equal("Ancestral Recall", firstCardNameTestFour.Name);
            Assert.Equal("Dark Ritual", secondCardNameTestFour.Name);
            Assert.Equal("Bad Moon", thirdCardNameTestFour.Name);

            // TestOneOne
            Assert.Equal(1, countTestOneOne);
            Assert.Equal("Crusade", cardNameTestOneOne.Name);

            // TestTwoTwo
            Assert.Equal(3, countTestTwoTwo);
            Assert.Equal("Gianth Growth", firstCardNameTestTwoTwo.Name);
            Assert.Equal("Healing Salve", secondCardNameTestTwoTwo.Name);
            Assert.Equal("Lightning Bolt", thirdCardNameTestTwoTwo.Name);

            // TestThreeThree
            Assert.Equal(1, countTestThreeThree);
            Assert.Equal("Crusade", firstCardNameTestThreeThree.Name);

            // TestFourFour
            Assert.Equal(18, countTestFourFour);
            Assert.Equal("Crusade", firstCardNameTestFourFour.Name);
            Assert.Equal("Dark Ritual", secondCardNameTestFourFour.Name);
            Assert.Equal("Bad Moon", thirdCardNameTestFourFour.Name);
            Assert.Equal("Mox Ruby", fourthCardNameTestFourFour.Name);
            Assert.Equal("Black Lotus", fifthCardNameTestFourFour.Name);

            // TestEmpty
            Assert.Equal(18, countTestEmpty);

            this.TearDownBase();
        }

        [Fact]
        public async Task ApprovePlayCardShouldChangeItsStatusToApproved()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCard = playCardRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == "5f3f96a8-836a-479c-93c8-6921feb79366");
            var statusBefore = playCard.ApprovedByAdmin;
            await service.ApproveCard("5f3f96a8-836a-479c-93c8-6921feb79366");
            var playCardAfter = playCardRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == "5f3f96a8-836a-479c-93c8-6921feb79366");
            var statusAfter = playCardAfter.ApprovedByAdmin;
            Assert.False(statusBefore);
            Assert.True(statusAfter);
            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveNonExistingPlayCardShouldReturnNull()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var result = await service.ApproveCard("5f3f96a8-836a-479c-93c8-6921feb79");

            Assert.Null(result);
            this.TearDownBase();
        }

        [Fact]
        public async Task DeleteCardShouldChangeTheTotalCountOfCardsAndWIllRemoveTheCard()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var cards = playCardRepository.AllAsNoTracking();
            var count = cards.Count();
            var specificCard = cards.FirstOrDefault(x => x.Name == "Mox Sapphire");

            await service.DeleteAsync(specificCard.Id);
            var countAfterDeletion = cards.Count();
            var specificCardNull = cards.FirstOrDefault(x => x.Name == "Mox Sapphire");

            Assert.Equal(19, count);
            Assert.Equal(18, countAfterDeletion);
            Assert.Null(specificCardNull);
            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardGetAllNonPaginationApprovedPlayCardsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var playCards = service.GetAllNoPagination<CardInListViewModel>();
            int playCardCount = playCards.Count();
            Assert.Equal(18, playCardCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task PlayCardExistShouldReturnTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var cards = playCardRepository.AllAsNoTracking();
            var specificCard = cards.FirstOrDefault(x => x.Name == "Mox Sapphire");
            var cardExist = await service.CardExist("5f3f96a8-836a-479c-93c8-6921feb79366");
            Assert.Equal("5f3f96a8-836a-479c-93c8-6921feb79366", specificCard.Id);
            Assert.True(cardExist);

            this.TearDownBase();
        }

        [Fact]
        public async Task HasUserWithIdShouldReturnTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var drawgoonId = "2738e787-5d57-4bc7-b0d2-287242f04695";
            var cardId = "5f3f96a8-836a-479c-93c8-6921feb79366"; ;
            bool cardExistByUserId = await service.HasUserWithIdAsync(cardId, drawgoonId);
            Assert.True(cardExistByUserId);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllEventCardsShouldReturnCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var eventCards = playCardRepository.AllAsNoTracking().Where(x => x.IsEventCard == true);
            var specificEventCard = eventCards.FirstOrDefault(x => x.Name == "Ancestral Recall");
            Assert.Equal(3, eventCards.Count());
            Assert.Equal("Ancestral Recall", specificEventCard.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetCardByNameShouldReturnCorrectCard()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var cards = playCardRepository.AllAsNoTracking();
            var eventCards = playCardRepository.AllAsNoTracking().Where(x => x.IsEventCard == true);
            var specificEventCard = service.GetByName<SingleCardViewModel>("Mox Sapphire");
            Assert.Equal("Mox Sapphire", specificEventCard.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCardsByExpansionShouldReturnCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var cardsFromExpansionOne = await service.GetAllCardsByExpansion<CardInListViewModel>(1);
            var cardsFromExpansionTwo = await service.GetAllCardsByExpansion<CardInListViewModel>(3);
            Assert.Equal(13, cardsFromExpansionOne.Count());
            Assert.Equal(6, cardsFromExpansionTwo.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllEventCardsShouldReturnTheCorrectCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var eventCards = playCardRepository.AllAsNoTracking().Where(x => x.IsEventCard == true);
            var specificEventCard = eventCards.FirstOrDefault(x => x.Name == "Ancestral Recall");
            Assert.Equal(3, eventCards.Count());
            Assert.Equal("Ancestral Recall", specificEventCard.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task DemoteAdCardShouldChangeTheExpansion()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var playCardRepository = new EfDeletableEntityRepository<PlayCard>(data);
            using var repositoryManaCost = new EfDeletableEntityRepository<ManaCost>(data);
            using var cardManaRepository = new EfDeletableEntityRepository<ManaInCard>(data);
            using var cardFrameColorRepository = new EfDeletableEntityRepository<PlayCardFrameColor>(data);
            using var cardTypeRepository = new EfDeletableEntityRepository<PlayCardType>(data);
            using var cardGameExpansionRepository = new EfDeletableEntityRepository<CardGameExpansion>(data);

            var fileService = new FileService();

            var service = new PlayCardService(
                playCardRepository,
                repositoryManaCost,
                cardManaRepository,
                cardFrameColorRepository,
                cardTypeRepository,
                cache,
                fileService);

            var testPlayCard = data.PlayCards.FirstOrDefault(x => x.Id == "cd83a0cb-c6d8-40cf-ad85-0aeede8ffd4a");

            await service.Promote(testPlayCard.Id);
            var currentExpansion = testPlayCard.CardGameExpansion.Title;

            await service.Demote(testPlayCard.Id);
            var newExpansion = testPlayCard.CardGameExpansion.Title;

            Assert.Equal("Second", currentExpansion);
            Assert.Equal("Beta", newExpansion);

            this.TearDownBase();
        }
    }
}

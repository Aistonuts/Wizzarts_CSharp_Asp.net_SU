namespace Wizzarts.Services.Data.Tests
{
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data;
    using Wizzarts.Services.Data.Tests.Mock;

    public class UnitTestBase : TestDbSeeder
    {
        public ApplicationDbContext dbContext;
        public IArticleService articleService;
        public ArtService artService;
        public IPlayCardService playCardService;
        public IStoreService storeService;
        public IEventService eventService;
        public IPlayCardExpansionService cardExpansionService;
        public IChatService chatService;

        public async void OneTimeSetup()
        {
            this.dbContext = DatabaseMock.MockDatabase();

            await this.SeedAsync(this.dbContext);
        }

        public async void TearDownBase() => await this.dbContext.DisposeAsync();
    }
}

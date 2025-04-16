﻿namespace Wizzarts.Services.Data.Tests
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

            //var cache = new MemoryCache(new MemoryCacheOptions());

            // using var repositoryArt = new EfDeletableEntityRepository<Art>(dbContext);
            // using var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            // using var repositoryPlayCard = new EfDeletableEntityRepository<PlayCard>(dbContext);
            // using var repositoryStore = new EfDeletableEntityRepository<Store>(dbContext);
            // using var repositoryEvent = new EfDeletableEntityRepository<Event>(dbContext);
            // using var repositoryCardExpansion = new EfDeletableEntityRepository<CardGameExpansion>(dbContext);
            // using var repositoryChat = new EfDeletableEntityRepository<ChatMessage>(dbContext);

            // var articlesService = new ArticleService(repositoryArticle, cache);
            // var artService = new ArtService(repositoryArt, cache);
            // var playCardService = new PlayCardService(repositoryPlayCard, repositoryArt, null, null, null, null, null, null, null, null, null, cache);
            // var storeService = new StoreService(repositoryStore);
            // var eventService = new EventService(repositoryEvent, null);
            // var cardExpansionService = new PlayCardExpansionService(repositoryCardExpansion);
            // var chatService = new ChatService(repositoryChat);
        }

        public async void TearDownBase() => await this.dbContext.DisposeAsync();
    }
}

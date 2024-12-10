namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new UserSeeder(),
                              new WizzartsCardGameSeeder(),
                              new WizzartsTeamRoleSeeder(),
                              new CardGameExpansionSeeder(),
                              new WizzartsTeamSeeder(),
                              new CardBlueManaSeeder(),
                              new CardBlackManaSeeder(),
                              new CardRedManaSeeder(),
                              new CardWhiteManaSeeder(),
                              new CardGreenManaSeeder(),
                              new CardColorlessManaSeeder(),
                              new PlayCardFrameColorSeeder(),
                              new PlayCardTypeSeeder(),
                              new PlayCardArtSeeder(),
                              new PlayCardSeeder(),
                              new ArticleSeeder(),
                              new AvatarSeeder(),
                              new TagHelpActionSeeder(),
                              new TagHelpControllerSeeder(),
                              new EventStatusSeeder(),
                              new EventCategorySeeder(),
                              new EventSeeder(),
                              new EventComponentSeeder(),
                              new StoreSeeder(),
                              new PlayCardManaSeeder(),
                              new WizzartsCardGameRulesSeeder(),
                              new WizzartsCardGameRulesDataSeeder(),
                              new ChatSeeder(),
                              new ChatUserSeeder(),
                              new ChatMessageSeeder(),
                              new DeckStatusSeeder(),
                              new DeckSeeder(),
                              new DeckOfCardsSeeder(),
                              new OrderStatusSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}

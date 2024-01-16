namespace MagicCardsmith.Data.Seeding
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
                              new SettingsSeeder(),
                              new UserSeeder(),
                              new PreAssignedRoleSeeder(),
                              new GameExpansionSeeder(),
                              new ArtistSeeder(),
                              new BlueManaSeeder(),
                              new BlackManaSeeder(),
                              new RedManaSeeder(),
                              new WhiteManaSeeder(),
                              new GreenManaSeeder(),
                              new ColorlessManaSeeder(),
                              new CardFrameSeeder(),
                              new CardTypeSeeder(),
                              new AlphaCardArtSeeder(),
                              new AlphaCardSeeder(),
                              new ArticleSeeder(),
                              new AvatarSeeder(),
                              new EventStatusSeeder(),
                              new EventSeeder(),
                              new EventMileStonesSeeder(),
                              new StoreSeeder(),
                              new ArticleSeeder(),
                              new CardManaSeeder(),
                              new GameRulesSeeder(),
                              new GameRulesComponentsSeeder(),
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

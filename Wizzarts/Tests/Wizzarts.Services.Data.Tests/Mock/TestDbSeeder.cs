namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Wizzarts.Data;
    using Wizzarts.Data.Seeding;
    using Wizzarts.Services.Data.Tests.Mock.TestData;
    using WWizzarts.Services.Data.Tests.Mock;

    public class TestDbSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            var seeders = new List<ITestDbSeeder>
                          {
                              //new RoleSeederTest(),
                              new UserTestSeeder(),
                              new WizzartsCardGameSeeder(),
                              //new WizzartsTeamRoleSeederTest(),
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
                              new EventStatusSeeder(),
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
                              new DeckSeederTest(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Data;
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
                              // new RoleSeederTest(),
                              new UserTestSeeder(),
                              new WizzartsCardGameSeeder(),

                              // new WizzartsTeamRoleSeederTest(),
                              new CardGameExpansionSeeder(),
                              new WizzartsTeamSeeder(),
                              new PlayCardFrameColorSeeder(),
                              new PlayCardTypeSeeder(),
                              new PlayCardArtSeeder(),
                              new PlayCardSeeder(),
                              new ArticleSeeder(),
                              new AvatarSeeder(),
                              new TagHelpActionSeeder(),
                              new TagHelpControllerSeeder(),
                              new EventCategorySeeder(),
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

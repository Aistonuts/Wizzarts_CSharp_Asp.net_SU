namespace Wizzarts.Services.Data.Tests.Mock
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class ChatUserSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.ChatUsers.Any())
            {
                return;
            }

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 1,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class ChatMessageSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.ChatMessages.Any())
            {
                return;
            }

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "General",
                Text = "Welcome to Wizzarts General chat.",
                Timestamp = DateTime.Now,
                ChatId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

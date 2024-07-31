using System;
using System.Linq;
using System.Threading.Tasks;
using Wizzarts.Data.Models.Enums;
using Wizzarts.Data.Models;
using Wizzarts.Data;

namespace Wizzarts.Services.Data.Tests.Mock
{
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

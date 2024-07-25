using System;
using System.Linq;
using System.Threading.Tasks;
using Wizzarts.Data.Models.Enums;
using Wizzarts.Data.Models;

namespace Wizzarts.Data.Seeding
{
    public class ChatMessageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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

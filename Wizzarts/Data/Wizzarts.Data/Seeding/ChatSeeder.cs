using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Models.Enums;

namespace Wizzarts.Data.Seeding
{
    public class ChatSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Chats.Any())
            {
                return;
            }

            await dbContext.Chats.AddAsync(new Chat
            {
               Name = "General",
               RelationKey = "General",
               Type = ChatType.Room,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

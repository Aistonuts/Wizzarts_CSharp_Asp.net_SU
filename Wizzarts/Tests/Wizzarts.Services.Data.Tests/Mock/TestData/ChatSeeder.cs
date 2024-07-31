using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data;
using Wizzarts.Data.Models;
using Wizzarts.Data.Models.Enums;

namespace Wizzarts.Services.Data.Tests.Mock
{
    public class ChatSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
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

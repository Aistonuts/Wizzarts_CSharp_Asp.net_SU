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

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "French",
                RelationKey = "French",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "Japanese",
                RelationKey = "Japanese",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "Spanish",
                RelationKey = "Spanish",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "German",
                RelationKey = "German",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "How to manage a game store ?",
                RelationKey = "How to.",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "How to become a professional artist ?",
                RelationKey = "How to.",
                Type = ChatType.Room,
            });

            await dbContext.Chats.AddAsync(new Chat
            {
                Name = "How to design a game and create game mechanics?",
                RelationKey = "How to.",
                Type = ChatType.Room,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

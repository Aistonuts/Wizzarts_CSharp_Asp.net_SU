using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Models.Enums;

namespace Wizzarts.Data.Seeding
{
    public class ChatUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 2,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 3,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 4,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 5,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 6,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 7,
            });

            await dbContext.ChatUsers.AddAsync(new ChatUser
            {
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ChatId = 8,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

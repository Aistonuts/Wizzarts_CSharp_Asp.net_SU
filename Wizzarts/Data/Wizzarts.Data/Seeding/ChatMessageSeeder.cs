namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

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

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "Français",
                Text = "Bienvenue sur le chat français de Wizzarts.",
                Timestamp = DateTime.Now,
                ChatId = 2,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "日本語",
                Text = "Wizzarts日本語チャットへようこそ。",
                Timestamp = DateTime.Now,
                ChatId = 3,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "Español",
                Text = "Bienvenido al chat en español de Wizzarts.",
                Timestamp = DateTime.Now,
                ChatId = 4,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "Deutsch",
                Text = "Willkommen beim deutschen Chat von Wizzarts.",
                Timestamp = DateTime.Now,
                ChatId = 5,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "How to manage a game store ?",
                Text = "Welcome to Wizzarts << How to manage a game store ? >> chat.",
                Timestamp = DateTime.Now,
                ChatId = 6,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "How to become a professional artist ?",
                Text = "Welcome to Wizzarts << How to become a professional artist ? >> chat.",
                Timestamp = DateTime.Now,
                ChatId = 7,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "How to design a game and create game engine?",
                Text = "Welcome to Wizzarts << How to design a game and create game engine? >> chat.",
                Timestamp = DateTime.Now,
                ChatId = 8,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "Request review of your Artist repository",
                Text = "All approved artist are invited to to join Wizzarts team.",
                Timestamp = DateTime.Now,
                ChatId = 9,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "Where is my order ? ",
                Text = "All about orders.",
                Timestamp = DateTime.Now,
                ChatId = 10,
            });

            await dbContext.ChatMessages.AddAsync(new ChatMessage
            {
                Name = "How to play Wizzarts the card game ? ",
                Text = "Everything about the rules of Wizzarts card game.",
                Timestamp = DateTime.Now,
                ChatId = 11,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

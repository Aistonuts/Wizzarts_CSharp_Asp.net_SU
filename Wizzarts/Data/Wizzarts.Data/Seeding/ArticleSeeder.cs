namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class ArticleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Wizzarts card game portal",
                ShortDescription = "Wizzarts website is the place where you can find all about the card game.",
                Description = "Wizzarts is the place where artists, content creators can directly contribute to the development of Wizzarts games.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/MagicTheGathering.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Wizzarts team.",
                ShortDescription = "This is our wizzarts team, store owners and artists.",
                Description = "We share common interest for art, tabletop games and collectible card games.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/SonsOfTheStorm.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.",
                ShortDescription = "Wizzarts card game alpha deck is out.",
                Description = "The alpha deck is available in our stores",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/art/Alpha/Crusade.png",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Wizzarts second card game second release events.",
                ShortDescription = "Participate in our events on the 1st of July 2024 to get access to more content",
                Description = "Event participants will be able create cards, add cards to deck they can pick later from Wizzarts stores close to them.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Champion.png",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Arena masters",
                ShortDescription = "Every battle needs a battleground.",
                Description = "If you are a store owner, passionate about board games and interested in hosting tournaments ,this is your chance. Click on become store owner," +
                "submit a picture of your store and fill your address, phone number and exact location.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Arena.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Call to arms",
                ShortDescription = "We are representing the current open positions. Artist, content creator, arena master.",
                Description = "Check our membership page for more information. Store owners are always welcome. Members who participate in our events have better chances in joining our team." +
                "For those who are not participating in our events, check the membership web page to find out more.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/WrenchMan.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });
            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Our newest event is Game testers.",
                ShortDescription = "Game testers will be invited to participate in the upcoming event which will take place at our Wizzarts stores.",
                Description = "As a game tester you need to create a deck, pick a delivery location, add cards and lock it. Locked card decsk will be shipped to your local Wizzarts store.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/AlphaBox.png",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });
            await dbContext.Articles.AddAsync(new Article
            {
                Title = "New cards have been added",
                ShortDescription = "We have added 6 cards which will be included in our beta deck.",
                Description = "The upcoming beta deck will be composed of cards created by our community. Participate in our events to get a chance to have one of your cards or art added to ur new deck.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/art/Unlimited/Black_lotus.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

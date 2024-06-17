namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

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
                Title = "Magic Cardsmith game is live.",
                ShortDescription = "Magic Cardsmith is the place where you can find all about our card game.",
                Description = "The Magic Cardsmith Alpha deck  has been just released. It is already avaialble in our stores.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/MagicTheGathering.jpg",
                ApprovedByAdmin = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Magic Cardsmith team.",
                ShortDescription = "Our team consist of artists, playgame testers, card flavour creators and store owners.",
                Description = "We share common interest for art, tabletob games and collectible card games.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/SonsOfTheStorm.jpg",
                ApprovedByAdmin = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.",
                ShortDescription = "Our Beta deck is almost complete.",
                Description = "Two months have passed since the release of our Aplha deck and it was a blast. " +
                "Since the release of our alpha deck we noticed your huge interest so we decided to release our Beta deck in the upcoming month," +
                "however we need your help.Before releasing the Beta deck we will allow you to participate in completing the deck by adding some of of your ideas.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/AlphaBox.png",
                ApprovedByAdmin = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Beta deck pre-release events",
                ShortDescription = "We are representing our first events board.The Magic Cardsmith community needs your help for releasing the upcomming expansion as soon as possible.",
                Description = "Our Beta deck is complete, but there is still place for your ideas." +
                "Upload your art, participate in our events, use our tools to create new play cards and we will include them in our final version of the Beta deck." +
                "Every card needs a certain type of flavour and an art of course, the art is what gives our cards their magical appearance" +
                "but the flavour is where their power resides." +
                "Keep in mind that there is a place in our team for those of you using their own art during the events.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Champion.png",
                ApprovedByAdmin = true,
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
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Call to arms",
                ShortDescription = "We are representing the current open positions. Artist, card flavour creator, arena master and game tester.",
                Description = "Do you see yourself as an artist? To find out, participate in our events and espacially those made for artists, where the card flavour and power is provided but there is noone worthy enough to wield them." +
                "Maybe you are a store owner interested in hosting tournaments ? Participating in our events where a new play mechanic need testing will be the best place to start.Each game needs an arena and each arena its Arena master." +
                "For those interested in testing their new play cards before submitting it we have provided a specia tool called Cardsmither which will allow you to forge your card and test it along with your other cards." +
                "Becoming a card flavour creator or a game tester is not an easy job, it needs teamwork. Teaming up is allowed.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/WrenchMan.jpg",
                ApprovedByAdmin = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

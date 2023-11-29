using MagicCardsmith.Data.Models;

namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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
                Title = "Greetings players",
                Description = "Magic: The Gathering (colloquially known as Magic or MTG) is a tabletop and digital collectible card game created by Richard Garfield.[1] " +
                "Released in 1993 by Wizards of the Coast (now a subsidiary of Hasbro)," +
                " Magic was the first trading card game and had approximately thirty-five million players as of December 2018," +
                "[2][3][4] and over twenty billion Magic cards were produced in the period from 2008 to 2016, during which time it grew in popularity.[5][6]",

            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Our team",
                Description = "Team made of artists. Check our art work.",

            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.Alpha deck is here",
                Description = "Our alpha deck is here and that is not all.We have not included the full set of cards." +
                "We have left some space for your Ideas.We have prepared two events for you , each with certain numbe of mile stones." +
                "We are prepared, we have the full set of cards ready but we decided to encourage our games creativity." +
                "Check the event page.Complete the milesstores.There might be a winner.It depends on how motivated you are.",

            });

        }
    }
}

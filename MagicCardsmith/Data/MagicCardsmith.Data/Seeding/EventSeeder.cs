using MagicCardsmith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Data.Seeding
{
    public class EventSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Events.Any())
            {
                return;
            }

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Add some flavor",
                EventDescription = "In celebration of the launch of the Alpha deck," +
                " we will allow few of you to contribute with the creation of our incomming game expansion." +
                "There are few art pieces in need of your help, use your imagination and provide them with special abilities and we will test them for you." +
                "The winners will be announced and their ideas will be included in our next deck",


            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Call to arms",
                EventDescription = "In celebration of the launch of the Alpha deck," +
               " we will allow few of you to contribute with the creation of our incomming game expansion." +
               "This will ook easy at first glance for some of you however you will have to compete with other artists." +
               "Bonus rewards for those who help us bring to life a new type of card,The planeswalker.Mighty commander with special abilities.",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
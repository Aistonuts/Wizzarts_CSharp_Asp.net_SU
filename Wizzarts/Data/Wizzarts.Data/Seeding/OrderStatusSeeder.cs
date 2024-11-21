namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wizzarts.Data.Models;

    public class OrderStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Pending",
            });

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Shipped",
            });

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Delivered",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

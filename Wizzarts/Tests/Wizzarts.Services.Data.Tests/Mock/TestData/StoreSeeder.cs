namespace Wizzarts.Services.Data.Tests.Mock
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class StoreSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Stores.Any())
            {
                return;
            }

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Bright",
                Country = "France",
                City = "Montpellier",
                Address = "23 Gabriel Peri 32000",
                PhoneNumber = "5674322111",
                Image = "/images/Stores/Bright.jpg",
                StoreOwnerId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ApprovedByAdmin = true,
            });

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Daytona Magic",
                Country = "USA",
                City = "Miami",
                Address = "34 Lower Keys 1900",
                PhoneNumber = "458-4567890",
                Image = "/images/Stores/Daytona_Magic.jpg",
                StoreOwnerId = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281",
                ApprovedByAdmin = true,
            });

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Wizards",
                Country = "France",
                City = "Toulouse",
                Address = "43 Jan Jaures 31000",
                PhoneNumber = "156754322",
                Image = "/images/Stores/Wizards.jpg",
                StoreOwnerId = "b4accad4-e878-4de3-a317-665d0a43fbd3",
                ApprovedByAdmin = true,
            });

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Wizards Headquarters Store",
                Country = "United States",
                City = "Renton, Washington,",
                Address = "45 Portsmouth 65000",
                PhoneNumber = "3224567890",
                Image = "/images/Stores/Wizards_Main.jpg",
                StoreOwnerId = "ad8dada2-c947-4ad3-aaa1-e530f13d21c1",
                ApprovedByAdmin = true,
            });

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Yokohama",
                Country = "Japan",
                City = "Yokohama",
                Address = "14 Sakura 34000",
                PhoneNumber = "1234567890",
                Image = "/images/Stores/Bright.jpg",
                StoreOwnerId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ApprovedByAdmin = true,
            });

            await dbContext.Stores.AddAsync(new Store
            {
                Name = "Bright",
                Country = "Japan",
                City = "Yokohama",
                Address = "14 Sakura 34000",
                PhoneNumber = "01-456-456-5600",
                Image = "/images/Stores/Yokohama.jpg",
                StoreOwnerId = "5823bbf1-993c-416b-9bf1-c358fedf38a6",
                ApprovedByAdmin = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

namespace MagicCardsmith.Services.Data.Tests.SeedTestDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data;

    public interface ITestDbSeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext);
    }
}

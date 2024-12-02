namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data;

    public interface ITestDbSeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext);
    }
}

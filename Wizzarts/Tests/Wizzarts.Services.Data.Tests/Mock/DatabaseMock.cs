namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data;

    public class DatabaseMock
    {
        public static ApplicationDbContext MockDatabase()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder
                         = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("ApplicationDbContextDb"
                    + DateTime.Now.Ticks.ToString());

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

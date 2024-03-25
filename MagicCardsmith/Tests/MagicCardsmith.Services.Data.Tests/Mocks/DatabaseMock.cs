namespace MagicCardsmith.Services.Data.Tests.Mocks
{
    using System;

    using MagicCardsmith.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class DatabaseMock
    {
        public static ApplicationDbContext MockDatabase()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder
                  = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("ApplicationDbContextDb"
                    + DateTime.Now.Ticks.ToString());

            return new ApplicationDbContext(optionsBuilder.Options, false);
        }
    }
}
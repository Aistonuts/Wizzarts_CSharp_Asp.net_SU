namespace Wizzarts.Services.Data.Tests.Mock
{
    using System.Threading.Tasks;

    using Wizzarts.Data;

    public interface ITestDbSeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext);
    }
}

namespace MagicCardsmith.Services.Data.Tests.UnitTests
{
    using MagicCardsmith.Data;
    using MagicCardsmith.Services.Data.Tests.Mocks;
    using MagicCardsmith.Services.Data.Tests.SeedTestDb;

    public class UnitTestsBase
    {
        protected ApplicationDbContext dbContext;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
           this.dbContext = DatabaseMock.MockDatabase();
        }

        [OneTimeTearDown]
        public void TearDownBase() => this.dbContext.Dispose();
    }
}

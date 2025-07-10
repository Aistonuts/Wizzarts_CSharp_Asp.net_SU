namespace Wizzarts.Services.Data.Tests.WizzartsServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.GameRules;
    using Wizzarts.Web.ViewModels.WizzartsMember;
    using Xunit;

    public class WizzartsServiceTest : UnitTestBase
    {
        public WizzartsServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetAllGameRulesData_Method_Should_Return_The_Correct_Count_Of_Rules_Components_And_The_First_Rule_Name_Should_Be_Correct()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var gameRules = await service.GetAllGameRulesData<GameRulesDataViewModel>();
            var firstRule = data.WizzartsGameRulesData.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(7, gameRules.Count());
            Assert.Equal("Creatures", firstRule.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllWizzartsTeamMembers_Method_Should_Return_Correct_Count_And_The_First_Member_Name_Should_Be_Correct()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var members = await service.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>();
            var firstMember = data.WizzartsTeamMembers.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(9, members.Count());
            Assert.Equal("Drawgoon", firstMember.Nickname);

            this.TearDownBase();
        }

        [Fact]
        public async Task GetGameRules_Method_Should_Return_The_Correct_Rule()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var gameRules = await service.GetGameRules<GameRulesViewModel>();

            Assert.Equal("Wizzarts card game rules", gameRules.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetUserIdByArtistId_Should_Return_The_Correct_UserId_We_Called_By_His_ArtistId()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var user = service.GetUserIdByArtistId(1);

            Assert.Equal("2738e787-5d57-4bc7-b0d2-287242f04695", user);
        }

        [Fact]
        public async Task Get_Non_Existing_UserId_ByArtistId_Should_Return_Null()

        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var user = service.GetUserIdByArtistId(20);

            Assert.Null(user);
        }
    }
}

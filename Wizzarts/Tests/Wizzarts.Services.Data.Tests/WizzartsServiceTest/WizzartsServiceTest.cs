using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.GameRules;
using Wizzarts.Web.ViewModels.WizzartsMember;
using Xunit;

namespace Wizzarts.Services.Data.Tests.WizzartsServiceTest
{
    public class WizzartsServiceTest : UnitTestBase
    {
        public WizzartsServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetAllGameRulesDataShouldReturnTheCorrectCountAndTheFirsRuleNameShouldBeCorrect()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository,gameRulesDataRepository,wizzartsTeamRepository);

            var gameRules = service.GetAllGameRulesData<GameRulesDataViewModel>();
            var firstRule = data.WizzartsGameRulesData.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(7,gameRules.Count());
            Assert.Equal("Creatures", firstRule.Title);
            TearDownBase();
        }

        [Fact]
        public async Task GetAllWizzartsTeamMembersShouldReturnCorrectCountAndTheFirstMemberNameShouldBeCorrect()
        {

            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var members = service.GetAllWizzartsTeamMembers<WizzartsTeamInListViewModel>();
            var firstMember = data.WizzartsTeamMembers.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(9, members.Count());
            Assert.Equal("Drawgoon", firstMember.Nickname);

            TearDownBase();
        }

        [Fact]
        public async Task GetAllGameRulesShouldReturnNameAndShouldBeCorrect()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var gameRulesRepository = new EfDeletableEntityRepository<WizzartsGameRules>(data);
            using var gameRulesDataRepository = new EfDeletableEntityRepository<WizzartsGameRulesData>(data);
            using var wizzartsTeamRepository = new EfDeletableEntityRepository<WizzartsTeam>(data);
            var service = new WizzartsServices(gameRulesRepository, gameRulesDataRepository, wizzartsTeamRepository);

            var gameRules = service.GetGameRules<GameRulesViewModel>();

            Assert.Equal("Wizzarts card game rules", gameRules.Title);
            TearDownBase();
        }

        [Fact]
        public async Task GetUserIdByArtistIdShouldReturnTheCorrectUserId()
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
        public async Task GetNonExistingUserIdByArtistIdShouldReturnNull()
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

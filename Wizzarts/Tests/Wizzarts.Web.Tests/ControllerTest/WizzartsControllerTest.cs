using MyTested.AspNetCore.Mvc;
using System.Linq;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Wizzarts.Web.ViewModels.GameRules;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class WizzartsControllerTest : UnitTestBase
    {
        [Fact]
        public void AllShouldReturnDefaultViewWithCorrectModel()
        {
            OneTimeSetup();
            var data = this.dbContext;
            MyController<WizzartsController>
                        .Instance(instance => instance
                            .WithData(data.WizzartsGameRules.ToList()))
                        .Calling(c => c.GetRules())
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<WizzartsCardGameViewModel>());

            TearDownBase();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Wizzarts.Services.Data.Tests;
using Wizzarts.Web.Controllers;
using Xunit;

namespace Wizzarts.Web.Tests.ControllerTest
{
    public class SearchControllerTest : UnitTestBase
    {
        [Fact]
        public void SearchTermShouldReturnCorrectCard()
        {
            MyController<SearchController>
                .Calling(s => s.Search())
                .ShouldReturn()
                .Ok();
        }
    }
}

namespace Wizzarts.Web.Tests.ControllerTest
{
    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Xunit;

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

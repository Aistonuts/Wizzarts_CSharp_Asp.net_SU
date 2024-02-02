using Microsoft.AspNetCore.Mvc;
using MagicCardsmith.Web.Controllers;
using MagicCardsmith.Services.Data;
using MagicCardsmith.Web.ViewModels.Card;
using Microsoft.AspNetCore.Authorization;
using MagicCardsmith.Services;

namespace SearchWebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly ISearchService searchService;

        public StatisticsApiController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel =
                    await searchService.GetStatisticsAsync();

                return Ok(serviceModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

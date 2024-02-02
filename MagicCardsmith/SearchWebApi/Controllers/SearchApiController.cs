using Microsoft.AspNetCore.Mvc;
using MagicCardsmith.Web.Controllers;
using MagicCardsmith.Services.Data;
using MagicCardsmith.Web.ViewModels.Card;
using Microsoft.AspNetCore.Authorization;

namespace SearchWebApi.Controllers
{
    [Route("api/searchCard")]
    [ApiController]
    public class SearchApiController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchApiController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [Produces("application/json")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["CardName"].ToString();
                var names = await this.searchService.GetAllCardsByTerm(term);

                return Ok(names);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}

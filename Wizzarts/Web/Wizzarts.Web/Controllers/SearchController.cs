using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Wizzarts.Services.Data;
using Wizzarts.Web.ViewModels.Search;
using Wizzarts.Web.ViewModels.PlayCard;

namespace Wizzarts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;
        private readonly IPlayCardService playCardService;

        public SearchController(
            ISearchService searchService, IPlayCardService playCardService)
        {
            this.searchService = searchService;
            this.playCardService = playCardService;

        }

        [Produces("application/json")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = this.HttpContext.Request.Query["CardName"].ToString();
                var names = await this.searchService.GetAllCardsByTerm(term);

                return Ok(names);
            }
            catch (Exception)
            {

                return this.BadRequest();
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Advanced(string cardName)
        {
            var card = this.playCardService.GetByName<SingleCardViewModel>(cardName);

            return this.RedirectToAction("ById", "PlayCard", new { id = card.Id });
        }
    }
}

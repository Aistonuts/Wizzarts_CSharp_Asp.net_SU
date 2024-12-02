namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.PlayCard;

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
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = this.HttpContext.Request.Query["CardName"].ToString();
                var names = await this.searchService.GetAllCardsByTerm(term);

                return this.Ok(names);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Advanced(string cardName)
        {
            var card = this.playCardService.GetByName<SingleCardViewModel>(cardName);

            return this.RedirectToAction("ById", "PlayCard", new { id = card.Id, information = card.GetCardName() });
        }
    }
}

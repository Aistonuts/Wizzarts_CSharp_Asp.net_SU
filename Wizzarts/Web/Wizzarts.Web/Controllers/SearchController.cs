namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Ganss.Xss;
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

        // searching for cards with autocomplete
        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = this.HttpContext.Request.Query["CardName"].ToString();
                var sanitizer = new HtmlSanitizer();
                var sanitizedText = sanitizer.Sanitize(term);
                var names = await this.searchService.GetAllCardsByTerm(sanitizedText);

                return this.Ok(names);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        // this will redirect to the actual card
        [HttpGet]
        public async Task<IActionResult> Advanced(string cardName)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(cardName);
            var card = this.playCardService.GetByName<SingleCardViewModel>(sanitizedText);

            if (card != null)
            {
                return this.RedirectToAction("ById", "PlayCard", new { id = card.Id, information = card.GetCardName() });
            }
            else
            {
                return this.RedirectToAction("All", "PlayCard");
            }
        }
    }
}

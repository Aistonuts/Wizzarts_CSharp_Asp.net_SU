namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.CardGameExpansion;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class ExpansionController : BaseController
    {
        private readonly IPlayCardService cardService;
        private readonly IDeckService deckService;
        private readonly IPlayCardExpansionService playCardExpansionService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpansionController(
             IPlayCardService cardService,
             IDeckService deckService,
             IPlayCardExpansionService playCardExpansionService,
             IEventService eventService,
             IArticleService articleService,

             UserManager<ApplicationUser> userManager)
        {
            this.cardService = cardService;
            this.deckService = deckService;
            this.playCardExpansionService = playCardExpansionService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var viewModel = new ExpansionListViewModel
            {
                CardGameExpansions = await this.playCardExpansionService.GetAll<ExpansionInListViewModel>(),
                Articles = this.articleService.GetRandom<ArticleInListViewModel>(4),
                Events = await this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ById(int id, string information)
        {
            var expansion = await this.playCardExpansionService.GetById<SingleExpansionViewModel>(id);

            if (expansion == null || information != expansion.GetExpansionTitle())
            {
                return this.BadRequest();
            }

            expansion.Cards = await this.cardService.GetAllCardsByExpansion<CardInListViewModel>(id);
            expansion.GameExpansions = await this.playCardExpansionService.GetAll<ExpansionInListViewModel>();

            return this.View(expansion);
        }

        // not used
        public async Task<IActionResult> Order(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.deckService.OrderAsync(id, this.User.GetId());
            }
            catch (Exception ex)
            {
                // return this.RedirectToAction(nameof(this.Add), new { id = id });
            }

            this.TempData["Message"] = "Order added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("My", "Order");
        }
    }
}

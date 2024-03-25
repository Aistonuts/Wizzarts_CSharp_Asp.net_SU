namespace MagicCardsmith.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ExpansionController : Controller
    {
        private readonly IExpansionService expansionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICardService cardService;
        private readonly IReviewService reviewService;

        public ExpansionController(
            IExpansionService expansionService,
            ICardService cardService,
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            this.expansionService = expansionService;
            this.cardService = cardService;
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            var expansion = this.expansionService.GetById<SingleExpansionViewModel>(id);
            expansion.Cards = this.cardService.GetAllCardsByExpansionId<CardInListViewModel>(id);
            expansion.CardReviews = this.reviewService.GetAllReviews<CardReviewInListViewModel>();
            return this.View(expansion);
        }
    }
}

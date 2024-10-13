using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Wizzarts.Data.Common.Repositories;
using Wizzarts.Data.Models;
using Wizzarts.Data;
using Wizzarts.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wizzarts.Web.ViewModels.CardGameExpansion;
using Wizzarts.Web.ViewModels.Expansion;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;

namespace Wizzarts.Web.Controllers
{
    public class ExpansionController : BaseController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPlayCardService cardService;
        private readonly ICommentService commentService;
        private readonly IPlayCardComponentsService playCardComponentsService;
        private readonly IPlayCardExpansionService playCardExpansionService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<PlayCard> cardRepository;

        public ExpansionController(
             ApplicationDbContext dbContext,
             IPlayCardService cardService,
             ICommentService commentService,
             IPlayCardComponentsService playCardComponentsService,
             IPlayCardExpansionService playCardExpansionService,
             IEventService eventService,
             IArticleService articleService,
             IArtService artService,
             UserManager<ApplicationUser> userManager,
             IWebHostEnvironment environment,
             IDeletableEntityRepository<PlayCard> cardRepository)
        {
            this.dbContext = dbContext;
            this.cardService = cardService;
            this.commentService = commentService;
            this.playCardComponentsService = playCardComponentsService;
            this.playCardExpansionService = playCardExpansionService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = new ExpansionListViewModel
            {
                CardGameExpansions = this.playCardExpansionService.GetAll<ExpansionInListViewModel>(),
                Articles = this.articleService.GetRandom<ArticleInListViewModel>(4),
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult ById(int id)
        {
            var expansion = this.playCardExpansionService.GetById<SingleExpansionViewModel>(id);

            expansion.PlayCards = this.cardService.GetAllCardsByExpansion<CardInListViewModel>(id);
            expansion.Expansions = this.playCardExpansionService.GetAll<ExpansionInListViewModel>();

            return this.View(expansion);
        }
    }
}

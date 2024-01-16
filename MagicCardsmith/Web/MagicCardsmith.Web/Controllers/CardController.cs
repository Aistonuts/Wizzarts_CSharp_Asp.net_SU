namespace MagicCardsmith.Web.Controllers
{

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Home;
    using MagicCardsmith.Web.ViewModels.Mana;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    public class CardController : Controller
    {
        private readonly ICardService cardService;
        private readonly ICategoryService categoryService;
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Card> cardRepository;


        public CardController(
            ICardService cardService,
            ICategoryService categoryService,
            IEventService eventService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Card> cardRepository)
        {
            this.cardService = cardService;
            this.categoryService = categoryService;
            this.eventService = eventService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;
        }

        public IActionResult Create(int id)
        {
            var viewModel = new CreateCardInputModel();
            viewModel.RedMana = this.categoryService.GetAllRedMana();
            viewModel.BlueMana = this.categoryService.GetAllBlueMana();
            viewModel.BlackMana = this.categoryService.GetAllBlackMana();
            viewModel.GreenMana = this.categoryService.GetAllGreenMana();
            viewModel.WhiteMana = this.categoryService.GetAllWhiteMana();
            viewModel.ColorlessMana = this.categoryService.GetAllColorlessMana();
            viewModel.SelectType = this.categoryService.GetAllCardType();
            viewModel.SelectFrameColor = this.categoryService.GetAllCardFrames();
            viewModel.SelectExpansion = this.categoryService.GetAllExpansionInListView();

            var milestone = this.eventService.GetMilestoneById<MilestonesInListViewModel>(id);
            var currentEvent = this.eventService.GetById<EventInListViewModel>(milestone.EventId);

            viewModel.EventMilestoneImage = milestone.ImageUrl;
            viewModel.EventMilestoneDescription = milestone.Description;
            viewModel.EventDescription = currentEvent.EventDescription;

            return this.View(viewModel);
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 5;
            var viewModel = new CardListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.cardService.GetCount(),
                Cards = this.cardService.GetAll<CardInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var card = this.cardService.GetById<SingleCardViewModel>(id);
            card.Mana = this.cardService.GetAllByCardId<ManaListViewModel>(id);
            return this.View(card);
        }
    }
}

namespace MagicCardsmith.Web.Controllers
{

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Home;
    using MagicCardsmith.Web.ViewModels.Mana;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    public class CardController : Controller
    {
        private readonly ICardService cardService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Card> cardRepository;


        public CardController(
            ICardService cardService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Card> cardRepository)
        {
            this.cardService = cardService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;
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

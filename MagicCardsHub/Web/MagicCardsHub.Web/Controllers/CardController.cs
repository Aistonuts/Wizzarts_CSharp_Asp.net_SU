namespace MagicCardsHub.Web.Controllers
{
    using MagicCardsHub.Data;
    using MagicCardsHub.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    public class CardController : Controller
    {
         private readonly ApplicationDbContext db;
         private readonly ICardService cardService;

         public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

      //  public IActionResult ById(int id)
      // {
      //   //  var playCard = this.playCardService.GetById<CreatePlayCardInputModel>(id);
      //   //  return this.View(playCard);
      // }
    }
}

namespace MagicCardsHub.Web.Controllers
{
    using MagicCardsHub.Data;
    using MagicCardsHub.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    public class PlayCardController : Controller
    {
         private readonly ApplicationDbContext db;
         private readonly IPlayCardService playCardService;

         public PlayCardController(IPlayCardService playCardService)
        {
            this.playCardService = playCardService;
        }

      //  public IActionResult ById(int id)
      // {
      //   //  var playCard = this.playCardService.GetById<CreatePlayCardInputModel>(id);
      //   //  return this.View(playCard);
      // }
    }
}

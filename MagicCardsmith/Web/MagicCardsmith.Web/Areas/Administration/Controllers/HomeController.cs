namespace MagicCardsmith.Web.Areas.Administration.Controllers
{
    using MagicCardsmith.Common;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Card;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdministrationController
    {
        private readonly ICardService cardService;

        public HomeController(
            ICardService cardService)
        {
            this.cardService = cardService;
        }

        public IActionResult Index( )
        {
            return this.View();
        }
    }
}

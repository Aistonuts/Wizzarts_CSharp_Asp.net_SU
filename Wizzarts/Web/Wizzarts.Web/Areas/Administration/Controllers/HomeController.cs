namespace Wizzarts.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdministrationController
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}

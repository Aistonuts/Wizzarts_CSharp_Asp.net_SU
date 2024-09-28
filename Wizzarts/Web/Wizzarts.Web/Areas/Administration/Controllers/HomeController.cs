using Microsoft.AspNetCore.Mvc;

namespace Wizzarts.Web.Areas.Administration.Controllers
{
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

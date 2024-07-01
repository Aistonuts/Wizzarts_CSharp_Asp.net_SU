using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Wizzarts.Web.ViewComponents
{
    public class SignUpViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

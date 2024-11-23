namespace Wizzarts.Web.Attributes
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Extensions;

    using static Wizzarts.Common.GlobalConstants;

    public class MustBePremiumAttribute : ActionFilterAttribute
    {

        private readonly UserManager<ApplicationUser> userManager;

        public async Task OnActionExecutingAsync(ActionExecutingContext context)
        {
        //    // Not working
        //    base.OnActionExecuting(context);

        //    var user = context.HttpContext.User;
        //    if (user == null)
        //    {
        //        context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }

        //    var loggedInUser = await this.userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)user);
        //    var currentRole = await this.userManager.GetRolesAsync(loggedInUser);

        //    if (user != null)
        //    {
        //        if (!currentRole.Contains(ArtistRoleName) || !currentRole.Contains(PremiumRoleName) || !currentRole.Contains(AdministratorRoleName))
        //        {
        //            context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
        //        }
        //    }
        }
    }
}

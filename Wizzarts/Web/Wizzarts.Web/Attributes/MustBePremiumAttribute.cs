namespace Wizzarts.Web.Attributes
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Wizzarts.Data.Models;

    using static Wizzarts.Common.GlobalConstants;

    public class MustBePremiumAttribute : ActionFilterAttribute
    {
        private readonly UserManager<ApplicationUser> userManager;

        public async Task OnActionExecutingAsync(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.HttpContext.User;
            if (user == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var loggedInUser = await this.userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)user);
            var currentRole = await this.userManager.GetRolesAsync(loggedInUser);

            if (user != null)
            {
                if (currentRole.Contains(ArtistRoleName) == false || !currentRole.Contains(PremiumRoleName) == false || !currentRole.Contains(AdministratorRoleName) == false || !currentRole.Contains(WizzartsTeamRoleName) == false)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
            }
        }
    }
}

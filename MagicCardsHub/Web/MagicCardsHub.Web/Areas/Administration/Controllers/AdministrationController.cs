namespace MagicCardsHub.Web.Areas.Administration.Controllers
{
    using MagicCardsHub.Common;
    using MagicCardsHub.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}

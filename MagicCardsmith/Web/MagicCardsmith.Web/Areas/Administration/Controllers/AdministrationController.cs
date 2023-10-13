namespace MagicCardsmith.Web.Areas.Administration.Controllers
{
    using MagicCardsmith.Common;
    using MagicCardsmith.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}

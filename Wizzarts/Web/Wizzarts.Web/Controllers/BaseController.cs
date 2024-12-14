namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Web.Attributes;

    [Authorize]
    public class BaseController : Controller
    {
    }
}

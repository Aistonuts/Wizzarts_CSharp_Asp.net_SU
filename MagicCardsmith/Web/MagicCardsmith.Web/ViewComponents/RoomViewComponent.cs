namespace MagicCardsmith.Web.ViewComponents
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Models.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext _ctx;

        public RoomViewComponent(ApplicationDbContext ctx)
        {
            this._ctx = ctx;
        }

        [AllowAnonymous]
        public IViewComponentResult Invoke()
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = this._ctx.Chats
                .ToList();

            return View(chats);
        }
    }
}

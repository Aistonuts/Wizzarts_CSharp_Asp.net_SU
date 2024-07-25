namespace MagicCardsmith.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Artist;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Home;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Mvc;
    using System.Web;
    using Microsoft.AspNetCore.Authorization;
    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Data.Models.Enums;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using MagicCardsmith.Web.ViewModels.Chat;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;
        private readonly IArtService artService;
        private readonly IArtistService artistService;
        private readonly ICardService cardService;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IExpansionService expansionService;
        private readonly ApplicationDbContext _ctx;

        public HomeController(
            IArticleService articlesService,
            IArtService artService,
            IArtistService artistService,
            ICardService cardService,
            IEventService eventService,
            IStoreService storeService,
            IExpansionService expansionService,
            ApplicationDbContext ctx)
        {
            this.articlesService = articlesService;
            this.artService = artService;
            this.artistService = artistService;
            this.cardService = cardService;
            this.eventService = eventService;
            this.storeService = storeService;
            this.expansionService = expansionService;
            this._ctx = ctx;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            var viewModel = new IndexViewModel
            {
                Articles = this.articlesService.GetRandom<IndexPageArticleViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Artists = this.artistService.GetRandom<ArtistInListViewModel>(4),
                Cards = this.cardService.GetRandom<CardInListViewModel>(3),
                Stores = this.storeService.GetAll<StoresInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.expansionService.GetAll<ExpansionInListViewModel>(),
                Chats = this._ctx.Chats.ToList(),
                Messages = this._ctx.Messages.ToList(), 
                //CardTypes = this.cardService.GetAllTypes<CardTypeIdViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return this.View("Error400");
            }

            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return View();

            //return this.View(
            //    new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            this._ctx.Chats.Add(new Chat
            {
                Name = name,
                Type = ChatType.Room,
            });
            await this._ctx.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Chat(int id)
        {
            var viewModel = new IndexViewModel
            {
                ChatId = id,
                Articles = this.articlesService.GetRandom<IndexPageArticleViewModel>(6),
                Arts = this.artService.GetRandom<ArtInListViewModel>(3),
                Artists = this.artistService.GetRandom<ArtistInListViewModel>(4),
                Cards = this.cardService.GetRandom<CardInListViewModel>(3),
                Stores = this.storeService.GetAll<StoresInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                GameExpansions = this.expansionService.GetAll<ExpansionInListViewModel>(),
                Chats = this._ctx.Chats.ToList(),
                Messages = this._ctx.Messages.Where(x => x.ChatId == id),
                //CardTypes = this.cardService.GetAllTypes<CardTypeIdViewModel>(),
            };

            var chat = _ctx.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(c => c.Id == id);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(
            int chatId,
            string message)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = "Default",
                Timestamp = DateTime.Now,
            };

            this._ctx.Messages.Add(Message);
            await this._ctx.SaveChangesAsync();
            return this.RedirectToAction("Index", new { id = chatId });
        }
    }
}

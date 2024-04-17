namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Mvc;

    public class StoreController : Controller
    {
        private readonly IStoreService storeService;

        public StoreController(
            IStoreService storeService)
        {
            this.storeService = storeService;
        }

        public IActionResult All( int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }
            const int ItemsPerPage = 4;
            var viewModel = new StoresListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.storeService.GetCount(),
                Stores = this.storeService.GetAll<StoresInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        //public IActionResult ById(string id)
        //{
        //    var store = this.storeService.GetById<Singlestore>(id);
        //    return this.View(art);
        //}
    }
}

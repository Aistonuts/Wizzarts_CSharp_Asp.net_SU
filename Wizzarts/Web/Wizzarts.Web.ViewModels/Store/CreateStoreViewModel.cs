namespace Wizzarts.Web.ViewModels.Store
{
    using Microsoft.AspNetCore.Http;

    public class CreateStoreViewModel : BaseStoreViewModel
    {
        public IFormFile StoreImage { get; set; }
    }
}

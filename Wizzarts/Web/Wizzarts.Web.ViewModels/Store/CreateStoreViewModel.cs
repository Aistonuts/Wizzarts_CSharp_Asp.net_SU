namespace Wizzarts.Web.ViewModels.Store
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CreateStoreViewModel : BaseStoreViewModel
    {
        [Required(ErrorMessage = "Store image is required!")]
        public IFormFile StoreImage { get; set; } = null!;
    }
}

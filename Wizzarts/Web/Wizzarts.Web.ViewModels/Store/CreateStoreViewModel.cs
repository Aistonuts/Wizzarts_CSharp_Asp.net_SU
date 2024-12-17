namespace Wizzarts.Web.ViewModels.Store
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateStoreViewModel : BaseStoreViewModel
    {
        [Required(ErrorMessage = "Store image is required!")]
        public IFormFile StoreImage { get; set; } = null!;
    }
}

namespace Wizzarts.Web.ViewModels.Deck
{
    using Microsoft.AspNetCore.Http;

    public class CreateDeckViewModel : BaseDeckViewModel
    {
        // [Required(ErrorMessage = "Art image is required!")]
        public IFormFile Image { get; set; }
    }
}

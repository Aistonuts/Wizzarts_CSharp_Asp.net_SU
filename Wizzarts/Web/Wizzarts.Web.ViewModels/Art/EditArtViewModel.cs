namespace Wizzarts.Web.ViewModels.Art
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class EditArtViewModel : BaseArtViewModel
    {
        [Required(ErrorMessage = "Art image is required!")]
        public IFormFile Image { get; set; }
    }
}

namespace Wizzarts.Web.ViewModels.Art
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddArtViewModel : BaseArtViewModel
    {
        [Required(ErrorMessage = "Art image is required!")]
        public IFormFile Image { get; set; }
    }
}

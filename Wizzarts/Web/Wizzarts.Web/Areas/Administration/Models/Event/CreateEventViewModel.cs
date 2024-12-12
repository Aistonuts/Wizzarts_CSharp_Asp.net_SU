namespace Wizzarts.Web.Areas.Administration.Models.Event
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateEventViewModel : BaseEventViewModel
    {
        [Required(ErrorMessage = "Event image is required!")]
        public IFormFile Image { get; set; }
    }
}

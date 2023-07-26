namespace MagicCardsHub.Web.ViewModels.BaseCreateModel
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class BaseCreateImageInputModel
    {
        public string ImageUrl { get; set; }

        public IEnumerable<IFormFile> Image { get; set; }
    }
}

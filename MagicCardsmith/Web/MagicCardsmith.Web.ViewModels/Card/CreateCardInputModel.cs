using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MagicCardsmith.Web.ViewModels.Card
{
    public class CreateCardInputModel : BaseCreateCardInputModel
    {
        public IFormFile Images { get; set; }
    }
}

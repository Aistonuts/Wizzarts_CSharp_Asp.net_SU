using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class CreateDeckViewModel : BaseDeckViewModel
    {
        //[Required(ErrorMessage = "Art image is required!")]
        public IFormFile Image { get; set; }
    }
}

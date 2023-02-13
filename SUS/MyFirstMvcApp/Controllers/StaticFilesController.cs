using SUS.HTTP;
using SUS.MvcFramework;
using System.IO;
using System.Net.Mime;

namespace MyFirstMvcApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            //var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            //var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);
            //return response;

            return this.File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");

        }
  
        internal HttpResponse BootstrapCss(HttpRequest arg)
        {
            return this.File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        internal HttpResponse BootstrapJs(HttpRequest arg)
        {
            return this.File("wwwroot/js/bootstrap.bundle.min.js", "text/javascript");
        }

        internal HttpResponse CustomCss(HttpRequest arg)
        {
            return this.File("wwwroot/css/custom.css", "text/css");
        }

        internal HttpResponse CustomJs(HttpRequest arg)
        {
            return this.File("wwwroot/js/custom.js", "text/javascript");
        }
        //ContentType browser should know the content type
    }
}

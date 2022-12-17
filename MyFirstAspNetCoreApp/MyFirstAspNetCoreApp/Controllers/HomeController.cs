using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyFirstAspNetCoreApp.Models;
using MyFirstAspNetCoreApp.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAspNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {        
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            
            this.ViewData["name"] = "Niki";     
            var viewModel = new IndexViewModel
            {
                Year = DateTime.UtcNow.Year,
                Message = "<script>alert('hacked')</script>",
                Names = new List<string> {"Niki","Stoyan" },
            };
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return this.Content(this.configuration["YouTube:ApiKey"]);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

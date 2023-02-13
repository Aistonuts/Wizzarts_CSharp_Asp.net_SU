using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Text;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            

            await Host.CreateHostAsync(new Startup(), 80);
        }


       

       

        

       

    }
}

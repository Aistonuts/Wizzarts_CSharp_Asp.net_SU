using System.Collections;
using System.Collections.Generic;

namespace MyFirstAspNetCoreApp.ViewModels.Home
{
    public class IndexViewModel
    {
        public string Message { get; set; }

        public int Year { get; set; }

        public IEnumerable<string> Names { get; set; }

        public string UpperMessage => this.Message.ToUpper();
    }
}

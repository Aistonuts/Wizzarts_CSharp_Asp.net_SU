
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Services.Data.Tests
{
    using MagicCardsmith.Web;
    using Microsoft.AspNetCore.Hosting;
    public class TestStartup : Program
    {
        public TestStartup(WebApplication app)
            : base(app)
        {
        }
    }
}

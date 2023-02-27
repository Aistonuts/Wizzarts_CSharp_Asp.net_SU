using MyWebServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class Startup
    {
        public static async Task Main(string[] args)
        {
            // http://localhost:9090

            var server = new HttpServer("127.0.0.1",9090);

            await server.Start();



        }
    }
   
}

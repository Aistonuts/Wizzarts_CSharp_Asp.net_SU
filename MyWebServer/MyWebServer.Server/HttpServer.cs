using System.Net;
using System.Net.Sockets;
using System.Text;
using MyWebServer.Server.Http;

namespace MyWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress, int port)
        {
             this.ipAddress = IPAddress.Parse("127.0.0.1");
             this.port = port;

              listener = new TcpListener(this.ipAddress, port);
        }

        public async Task Start()
        {
            

            this.listener.Start();

            Console.WriteLine(($"Server started on port {port}..."));
            Console.WriteLine("Listening for request...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();
            //var totalBytesRed = 0;
            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                // totalBytesRed + = bytesRead;
                // if (totalBytesRed > 10 * 1024)
                // {
                //     connection.Close();
                // }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            }

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = "Здрасти!";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"
HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow.ToString(("r"))}
Content-length: {contentLength}
Content-type: text/plain; charset=UTF8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}

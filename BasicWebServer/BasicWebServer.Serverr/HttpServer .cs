using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        private readonly RoutingTable routingTable;

        public HttpServer(string _ipAddress, int _port, Action<RoutingTable> routingTableConfiguration)
        {
            ipAddress = IPAddress.Parse(_ipAddress);
            port = _port;

            serverListener = new TcpListener(ipAddress, port);

            routingTableConfiguration(this.routingTable = new RoutingTable());

        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            :this("127.0.0.1", port, routingTable)
        {

        }

        public HttpServer( Action<IRoutingTable> routingTable)
           : this(8080, routingTable)
        {

        }

        public void Start()
        {
            serverListener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine($"Listening for requests...");

            while (true)
            {
                var connection = serverListener.AcceptTcpClient();

                var networkStream = connection.GetStream();

                var requestText = this.ReadRequest(networkStream);
                Console.WriteLine(requestText);

                var request = Request.Parse(requestText);

                var response = this.routingTable.MatchRequest(request);

               

                if (response.PreRenderAction!=null)
                {
                    response.PreRenderAction(request, response);
                }

                WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private void WriteResponse(NetworkStream networkStream, Response response)
        {
           // var contentLength = Encoding.UTF8.GetByteCount(message);

            //var response = $@"HTTP/1.1 200 OK
//Content-Type: text/plain; charset=UTF-8
//Content-Lenght: {contentLength}

//{message}";

            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            networkStream.Write(responseBytes);
        }

        private string ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalbytes = 0;
            var requestBuilder = new StringBuilder();



            do
            {
                var bytesRead = networkStream.Read(buffer, 0, bufferLength);
                totalbytes += bytesRead;

                if (totalbytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();

            //can crash in internet, //https://stackoverflow.com/questions/4261365

        }
    }
}

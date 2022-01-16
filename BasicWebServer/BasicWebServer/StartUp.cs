



//var ipAddress = IPAddress.Parse("127.0.0.1");

//var port = 8080;
//var serverListener = new TcpListener(ipAddress, port);

//serverListener.Start();

//Console.WriteLine($"Server started on port {port}");
//Console.WriteLine($"Listening for requests...");



//while (true)
//{
//    var connection = serverListener.AcceptTcpClient();

//    var networkStream = connection.GetStream();

//    var content = "Hello from the server";
//    var contentLength = Encoding.UTF8.GetByteCount(content);

//    var response = $@"HTTP/1.1 200 OK
//Content-Type: text/plain; charset=UTF-8
//Content-Lenght: {contentLength}

//{content}";

//    var responseBytes = Encoding.UTF8.GetBytes(response);

//    networkStream.Write(responseBytes);

//    connection.Close();
//}

//var server = new HttpServer("127.0.0.1", 8080);
//server.Start(); 

using BasicWebServer.Serverr;
using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Responses;

public class StartUp
{
    private const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name ='Age'/>
<input type='submit' value ='Save' />
</form>";

    public static void Main()
    {
        new HttpServer(routes => routes
        .MapGet("/", new TextResponse("Hello from the Server!"))
        .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))
        .MapGet("/HTML", new HtmlResponse(StartUp.HtmlForm))
        .MapPost("/HTML", new TextResponse("", StartUp.AddFormDataAction)))
            .Start();
    }

    private static void AddFormDataAction(Request request, Response response)
    {
        response.Body = "";

        foreach (var (key, value)  in request.Form)
        {
            response.Body += $"{key} - {value}";
            response.Body += Environment.NewLine;
        }
    }
}







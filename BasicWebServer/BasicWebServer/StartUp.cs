using BasicWebServer.Demo.Controllers;
using BasicWebServer.Demo.Services;
using BasicWebServer.Serverr;
using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Routing;


public class StartUp
{
    public static async Task Main()
    {

        var server = new HttpServer(routes => routes
             .MapControllers());

        server.ServiceCollection
            .Add<UserService>();

        await server.Start();


    }
    
}







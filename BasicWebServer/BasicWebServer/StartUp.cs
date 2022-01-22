﻿



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
using System.Text;
using System.Web;

public class StartUp
{
    private const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name ='Age'/>
<input type='submit' value ='Save' />
</form>";

    private const string DownloadForm = @"<form action='/Content' method='POST'>
   <input type='submit' value ='Download Sites Content' /> 
</form>";

    private const string FileName = "content.txt";

    private const string LoginForm = @"<form action='/LogIn' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='text' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";

    private const string Username = "user";
    private const string Password = "user123";


    public static async Task Main()
    {
        await DownloadSiteAsTextFile(StartUp.FileName, new string[] { "https://judge.softuni.org/", "https://softuni.org/" });

        var server = new HttpServer(routes => routes
        .MapGet("/", new TextResponse("Hello from the Server!"))
        .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))
        .MapGet("/HTML", new HtmlResponse(StartUp.HtmlForm))
        .MapPost("/HTML", new TextResponse("", StartUp.AddFormDataAction))
        .MapGet("/Content", new HtmlResponse(StartUp.DownloadForm))
        .MapPost("/Content", new TextFileResponse(StartUp.FileName))
        .MapGet("/Cookies", new HtmlResponse("", StartUp.AddCookiesAction))
        .MapGet("/Session", new TextResponse("", StartUp.DisplaySessionInfoAction))
        .MapGet("/LogIn", new HtmlResponse(StartUp.LoginForm))
        .MapPost("/LogIn", new HtmlResponse("", StartUp.LoginAction))
        .MapGet("/Logout", new HtmlResponse("", StartUp.LogoutAction))
        .MapGet("/UserProfile", new HtmlResponse("", StartUp.GetUserDataAction)));

        await server.Start();
    }

    private static void GetUserDataAction(Request request, Response response)
    {
        if (request.Session.ContainsKey(Session.SessionUserKey))
        {
            response.Body = "";
            response.Body += $"<h3>Currently logged - in user " + $"is with username '{Username}'</h3>";
        }
        else
        {
            response.Body = "";
            response.Body += $"<h3>You should first log in" + $"- <a href='/LogIn'>LogIn</a></h3>";
        }
    }

    private static void LogoutAction(Request request, Response response)
    {
       request.Session.Clear();
        response.Body = "";
        response.Body += "<h3> Logged out successfully!</h3>";
    }

    private static void LoginAction(Request request, Response response)
    {
        request.Session.Clear();

        var bodyText = "";
        var usernameMatches = request.Form["Username"] == StartUp.Username;
        var passwordMatches = request.Form["Password"] == StartUp.Password;

        if (usernameMatches && passwordMatches)
        {
            request.Session[Session.SessionUserKey] = "MyUserId";
            response.Cookies.Add(Session.SessionCookieName, request.Session.Id);

            bodyText = "<h3> Logged successfully!</h3>";
        }
        else
        {
            bodyText = StartUp.LoginForm+"<h4>Password is not correct! Try again!</h4>"; //add my response message
        }

        response.Body = "";
        response.Body += bodyText;
    }

    private static void AddFormDataAction(Request request, Response response)
    {
        response.Body = "";

        foreach (var (key, value) in request.Form)
        {
            response.Body += $"{key} - {value}";
            response.Body += Environment.NewLine;
        }
    }

    private static void AddCookiesAction(Request request, Response response)
    {
        var requesHasCookies = request.Cookies.Any(c => c.Name != Session.SessionCookieName);

        var bodyText = "";
        if (requesHasCookies)
        {
            var cookieText = new StringBuilder();
            cookieText.AppendLine("<h1>Cookies</>");
            cookieText.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

            foreach (var cookie in request.Cookies)
            {
                cookieText.Append("<tr>");
                cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>")
                    .Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                cookieText.Append("</tr>");
            }

            cookieText.Append("</table>");
            bodyText = cookieText.ToString();
        }
        else
        {
            bodyText = "<h1>Cookies Set!</h1>";
        }
        response.Body = bodyText;

        if (!requesHasCookies)
        {
            response.Cookies.Add("My-Cookie", "My-Value");
            response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
        }
    }

    private static async Task<string> DownloadWebSiteContent(string url)
    {
        var httpClient = new HttpClient();

        using (httpClient)
        {
            var response = await httpClient.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();

            return html.Substring(0, 2000);
        }
    }

    private static async Task DownloadSiteAsTextFile(string fileName, string[] urls)
    {
        var downloads = new List<Task<string>>();

        foreach (var url in urls)
        {
            downloads.Add(DownloadWebSiteContent(url));
        }

        var responses = await Task.WhenAll(downloads);

        var responsesString = string.Join(Environment.NewLine + new String('-', 100), responses);

        await File.WriteAllTextAsync(fileName, responsesString);



    }

    private static void DisplaySessionInfoAction(Request request, Response response)
    {
        var sessionExists = request.Session.ContainsKey(Session.SessionCurrentDateKey);

        var bodyText = "";

        if (sessionExists)
        {
            var currentDate = request.Session[Session.SessionCurrentDateKey];
            bodyText = $"Stored date: {currentDate}!";
        }
        else
        {
            bodyText = "Current date stored!";
        }
        response.Body = "";
        response.Body += bodyText;
    }
}







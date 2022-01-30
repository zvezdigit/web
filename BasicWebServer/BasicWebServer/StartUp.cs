﻿using BasicWebServer.Demo.Controllers;
using BasicWebServer.Serverr;
using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Routing;


public class StartUp
{
    public static async Task Main()
    {

        await new HttpServer(routes => routes
        .MapGet<HomeController>("/", c => c.Index())
        .MapGet<HomeController>("/Redirect", c => c.Redirect())
        .MapGet<HomeController>("/HTML", c => c.Html())
        .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
        .MapGet<HomeController>("/Content", c => c.Content())
        .MapPost<HomeController>("/Content", c => c.DownloadContent())
        .MapGet<HomeController>("/Cookies", c => c.Cookies())
        .MapGet<HomeController>("/Session", c => c.Session())
        .MapGet<UserController>("/LogIn", c => c.Login())
        .MapPost<UserController>("/LogIn", c => c.LogInUser())
        .MapGet<UserController>("/Logout", c => c.LogOut())
        .MapGet<UserController>("/UserProfile", c => c.GetUserData()))
        .Start();


    }
    
}







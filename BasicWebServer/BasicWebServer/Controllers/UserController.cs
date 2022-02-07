using BasicWebServer.Demo.Services;
using BasicWebServer.Serverr.Controllers;
using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.HTTP.Attributes;

namespace BasicWebServer.Demo.Controllers
{
    public class UserController : Controller
    {
       

        [HttpPost]
        public Response Login()
        {
            return View();
        }


        private readonly UserService userService;
        public UserController(Request request, UserService _userService)
            : base(request)
        {
            userService = _userService;
        }
        public Response LogInUser()
        {
            this.Request.Session.Clear();

            var username = Request.Form["Username"];
            var password = Request.Form["Password"];

            if (userService.IsLoginCorrect(username, password))
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    SignIn(Guid.NewGuid().ToString());


                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, Request.Session.Id);

                    return Html("<h3> Logged successfully!</h3>", cookies);
                }
               
               return Html("<h3> Logged successfully!</h3>");
            }
            return Redirect("/Login");
        }

        public Response Logout()
        {
            SignOut();

            return Html("<h3> Logged out successfully!</h3>");
        }

        [Authorize]
        public Response GetUserData()
        {
            return Html($"<h3>Currently logged-in user is with id '{User.Id}'</h3>");
        }

    }
}

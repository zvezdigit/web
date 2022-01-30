using BasicWebServer.Serverr.Controllers;
using BasicWebServer.Serverr.HTTP;


namespace BasicWebServer.Demo.Controllers
{
    public class UserController : Controller
    {
        public UserController(Request request) 
            : base(request)
        {

        }

        private const string LoginForm = @"<form action='/LogIn' method='POST'>
        Username: <input type='text' name='Username'/>
        Password: <input type='text' name='Password'/>
        <input type='submit' value ='Log In' /> 
        </form>";
        public Response Login()
        {
            return Html(LoginForm);
        }


        private const string Username = "user";
        private const string Password = "user123";
        public Response LogInUser()
        {
            this.Request.Session.Clear();

            var usernameMatches = Request.Form["Username"] == UserController.Username;
            var passwordMatches = Request.Form["Password"] == UserController.Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, Request.Session.Id);

                    return Html("<h3> Logged successfully!</h3>", cookies);
                }
               
               return Html("<h3> Logged successfully!</h3>");
            }
            return Redirect("/Login");
        }

        public Response LogOut()
        {
            this.Request.Session.Clear();

            return Html("<h3> Logged out successfully!</h3>");
        }

        public Response GetUserData()
        {
            if (Request.Session.ContainsKey(Session.SessionUserKey))
            {
              return Html( $"<h3>Currently logged - in user " + $"is with username '{UserController.Username}'</h3>");
            }
            return Redirect("/LogIn");
        }

    }
}

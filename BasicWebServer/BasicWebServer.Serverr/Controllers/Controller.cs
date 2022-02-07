using BasicWebServer.Demo.Views;
using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.Controllers
{
    public abstract class Controller
    {
        public Controller(Request request)
        {
            this.Request = request;
        }

        protected  Request Request { get; set; }

        protected Response Text(string text) => new TextResponse(text);
        protected Response Html(string html, CookieCollection cookies=null)
        {
            var response = new HtmlResponse(html);


            if (cookies!=null)
            {
                foreach (var cookie in cookies)
                {
                    response.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return response;
        }

        protected Response View([CallerMemberName] string viewName = "")
        {
            return new ViewResponse(viewName, this.GetControllerName());
        }

        protected Response View(object model, [CallerMemberName] string viewName = "")
        {
            return new ViewResponse(viewName, this.GetControllerName(), model);
        }

        private string GetControllerName()
        {
            return this.GetType().Name
                .Replace(nameof(Controller), string.Empty);
        }

        protected Response BadRequest() => new BadRequestResponse();
        protected Response Unauthorized() => new UnAuthorizedResponse();
        protected Response NotFound() => new NotFoundResponse();
        protected Response Redirect(string location) => new RedirectResponse(location);
        protected Response File(string fileName) => new TextFileResponse(fileName);

    }
}

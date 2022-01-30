using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Demo.Views
{
    public class ViewResponse : ContentResponse
    {
        private const char PathSeparator = '/';
        public ViewResponse(string viewName, string controllerName, object model=null) 
            : base("", ContentType.Html )
        {

            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.Combine("Views", viewName) + ".cshtml";

            //var viewPath = Path.GetFullPath($"./Views/"
            //    + viewName.TrimStart(PathSeparator)
            //    + ".cshtml");

            var viewContent = File.ReadAllText(viewPath);

            if (model!=null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.Body = viewContent;
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                 .GetType()
                 .GetProperties()
                 .Select(p => new
                 {
                     p.Name,
                     Value = p.GetValue(model)
                 });

            foreach (var entry in data)
            {
                const string openingBrackets = "{{";
                const string closingBrackets = "}}";

                viewContent = viewContent.Replace($"{openingBrackets}{entry.Name}{ closingBrackets}",
                    entry.Value.ToString());
            }

            return viewContent;
        }
    }
}

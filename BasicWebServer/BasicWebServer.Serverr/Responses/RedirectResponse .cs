using BasicWebServer.Serverr.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.Responses
{
    public class RedirectResponse: Response
    {
        public RedirectResponse(string location)
            :base(StatusCode.Found)
        {
            this.Headers.Add(Header.Location, location);
        }
    }
}

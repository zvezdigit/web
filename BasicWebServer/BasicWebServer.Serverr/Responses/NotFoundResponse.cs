using BasicWebServer.Serverr.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.Responses
{
    public class NotFoundResponse: Response
    {
        public NotFoundResponse()
            :base(StatusCode.NotFound)
        {

        }
    }
}

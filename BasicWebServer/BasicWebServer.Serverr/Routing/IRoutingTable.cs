﻿using BasicWebServer.Serverr.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(Method method, string path, Func<Request, Response> responseFunction);
        IRoutingTable MapGet(string path, Func<Request, Response> responseFunction);
        IRoutingTable MapPost(string path, Func<Request, Response> responseFunction);
    }
}

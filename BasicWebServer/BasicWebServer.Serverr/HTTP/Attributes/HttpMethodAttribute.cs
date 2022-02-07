﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.HTTP.Attributes
{

        [AttributeUsage(AttributeTargets.Method)]
        public abstract class HttpMethodAttribute : Attribute
        {
            public Method HttpMethod { get; }

            protected HttpMethodAttribute(Method httpMethod)
                => HttpMethod = httpMethod;
        }
    
}

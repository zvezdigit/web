﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr
{
    public static  class Guard
    {
        public static void AgainstNull(object value, string name =null)
        {

            if (value==null)
            {
                name ??= "Value";
                throw new ArgumentNullException($"{name} can not be null"); 
            }
        }
    }
}

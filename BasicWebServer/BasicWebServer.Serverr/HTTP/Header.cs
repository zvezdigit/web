using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.HTTP
{
    public class Header
    {
        public Header(string _name, string _value)
        {
            Guard.AgainstNull(_name, nameof(_name));
            Guard.AgainstNull(_value, nameof(_value));
            Name = _name;       
            Value = _value; 
        }
        public string Name { get; init; }    
        public string Value { get; set; }

        public const string ContentType = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string ContentDisposition = "Content-Disposition";
        public const string Cookie = "Cookie";
        public const string Date = "Date";
        public const string Location = "Location";
        public const string Server = "Server";
        public const string SetCookie = "Set-Cookie";

        public override string ToString()
            => $"{this.Name}: {this.Value}";
    }
}

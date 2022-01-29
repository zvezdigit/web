using BasicWebServer.Serverr.HTTP;
using BasicWebServer.Serverr.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Serverr.Routing
{
    public class RoutingTable: IRoutingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Func<Request, Response>>> routes;

        public RoutingTable()
            => this.routes = new ()
            {

                [Method.Get] = new (StringComparer.InvariantCultureIgnoreCase),
                [Method.Post] = new (StringComparer.InvariantCultureIgnoreCase),
                [Method.Put] = new (StringComparer.InvariantCultureIgnoreCase),
                [Method.Delete] = new (StringComparer.InvariantCultureIgnoreCase),
            };

        public IRoutingTable Map (Method method, string path, Func<Request, Response> responseFunction)
            //=> method switch
            {

            //Method.Get=>this.MapGet(url, response),
            //Method.Post=>this.MapPost(url, response),
            //_=>throw new InvalidOperationException($"Method {method} is not supported")

            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            switch (method)
            {
                case Method.Get: 
                    return MapGet(path, responseFunction);
                case Method.Post:
                    return MapPost(path, responseFunction);
                case Method.Put:
                case Method.Delete:
                default:
                    throw new ArgumentOutOfRangeException($"This method {nameof(method)} is not supported!");
            }

            //this.routes[method][path] = responseFunction;
            //return this;
        }

        public IRoutingTable MapGet(string path, Func<Request, Response> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[Method.Get][path] = responseFunction;

            return this;
        }

        public IRoutingTable MapPost(string path, Func<Request, Response> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[Method.Post][path] = responseFunction;

            return this;
        }

        public Response MatchRequest(Request request)
        {

            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestUrl))
            {

                return new NotFoundResponse();
            }

            var responseFunction = this.routes[requestMethod][requestUrl];

            return responseFunction(request);
        }
    }
}

﻿using BasicWebServer.Serverr.HTTP;
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
        private readonly Dictionary<Method, Dictionary<string, Response>> routes;

        public RoutingTable()
            => this.routes = new Dictionary<Method, Dictionary<string, Response>>()
            {

                [Method.Get] = new Dictionary<string, Response>(StringComparer.InvariantCultureIgnoreCase),
                [Method.Post] = new Dictionary<string, Response>(StringComparer.InvariantCultureIgnoreCase),
                [Method.Put] = new Dictionary<string, Response>(StringComparer.InvariantCultureIgnoreCase),
                [Method.Delete] = new Dictionary<string, Response>(StringComparer.InvariantCultureIgnoreCase),
            };

        public IRoutingTable Map(string url, Method method, Response response)
            => method switch
            {

                Method.Get=>this.MapGet(url, response),
                Method.Post=>this.MapPost(url, response),
                _=>throw new InvalidOperationException($"Method {method} is not supported")
            };

        public IRoutingTable MapGet(string url, Response response)
        {
           Guard.AgainstNull(url,nameof(url));
           Guard.AgainstNull(response,nameof(response));

            this.routes[Method.Get][url] = response;

            return this;
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[Method.Post][url] = response;

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

            return this.routes[requestMethod][requestUrl];
        }
    }
}

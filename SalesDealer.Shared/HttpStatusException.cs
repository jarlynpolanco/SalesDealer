using System;
using System.Net;

namespace SalesDealer.Shared
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public HttpStatusException(string message, HttpStatusCode statusCode) : base(message) =>
            this.StatusCode = statusCode;
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SalesDealer.Shared
{
    public class HttpStatusCodeException
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeException(RequestDelegate next) =>
            this._next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusException ex)
            {
                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)(ex.StatusCode);
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new GenericResponse<string>
                {
                    Message = ex.Message
                });

                await context.Response.WriteAsync(result);
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new GenericResponse<string>
                {
                    Message = ex.Message
                });

                await context.Response.WriteAsync(result);
                return;
            }
        }
    }
}

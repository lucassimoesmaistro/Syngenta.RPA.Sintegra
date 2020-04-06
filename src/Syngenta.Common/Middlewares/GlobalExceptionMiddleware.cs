using Syngenta.Common.Log;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Syngenta.Common.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        readonly RequestDelegate _requestDelegate;

        public GlobalExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            try
            {
                await _requestDelegate(httpContext);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            Logger.Logar.Error(exception, "GlobalExceptionMiddleware");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                source = exception.Source,
                message = exception.Message,
                timeStamp = DateTime.Now
            }));

        }
    }
}

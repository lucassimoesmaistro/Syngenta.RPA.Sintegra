using Syngenta.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Syngenta.RPA.Sintegra.Bootstrapper
{
    public static class ExceptionHandlerInitializer
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}

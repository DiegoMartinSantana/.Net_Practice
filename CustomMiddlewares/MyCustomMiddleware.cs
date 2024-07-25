
using Microsoft.AspNetCore.SignalR;

namespace NetCoreProyectExample.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Im a customer middleware - First\n");
            await next(context);
            await context.Response.WriteAsync("Im a customer middleware - END\n");

        }
    }
    //if i dont want Use app in program , can use extension methods

    public static class MyCustomMiddlewareExtension
    {
        //with that we are able to call this methods with APP 
        public static IApplicationBuilder UseCustomMiddleware1(this IApplicationBuilder app)
        {
           return app.UseMiddleware<MyCustomMiddleware>();
           
        }



    }
}

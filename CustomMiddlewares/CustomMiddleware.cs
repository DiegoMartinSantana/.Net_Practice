using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NetCoreProyectExample.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //before logic
            if(httpContext.Request.Query.ContainsKey("name")&&
                httpContext.Request.Query.ContainsKey("surname"))
            {
                string name = httpContext.Request.Query["name"];
                string surname = httpContext.Request.Query["surname"];
                string people = name+ " " + surname;
              await  httpContext.Response.WriteAsync("people : " +  people);
            }
            else
            {
                await httpContext.Response.WriteAsync("parameters invalid\n");

            }
                await _next(httpContext);

            //after logic
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseClassCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}

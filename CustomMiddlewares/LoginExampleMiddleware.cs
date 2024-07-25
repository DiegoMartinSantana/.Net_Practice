using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace NetCoreProyectExample.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string method = httpContext.Request.Method;
            if (method == "POST")
            {
                //read the body
                httpContext.Response.Headers["Content-Type"] = "text/html";
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                var body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues>
                  Dict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);


                if (Dict.ContainsKey("username") && Dict.ContainsKey("pass"))
                {
                    string pass = Dict["pass"][0];
                    string username = Dict["username"][0];
                    if (pass == "123" && username == "pepe")
                    {
                        httpContext.Response.StatusCode = 200;

                        await httpContext.Response.WriteAsync("successful login");

                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;

                        await httpContext.Response.WriteAsync("invalid login");
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 400;

                    await httpContext.Response.WriteAsync("very bad request");
                }
            }


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiniLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginExampleMiddleware>();
        }
    }
}

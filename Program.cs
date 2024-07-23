using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Primitives;

namespace NetCoreProyectExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create builder for web app
            var builder = WebApplication.CreateBuilder(args);
            //with that we can configure the web app or access to the services
            //builder.Configuration
            //
            //builder.Services
            var app = builder.Build();//Build web app(config, default services etc)
                                      // .Build return a instance of object web app
                                      // app.MapGet("/", () => "Hi Future Developer!");

            //app run recibe un argumento  : httpcontext
            app.Run(async (HttpContext context) =>
            {

                #region Class Http (3)
                /*
                context.Response.Headers["Content-Type"] = "text/html";

                //if i want read the body request :
                System.IO.StreamReader reader = new StreamReader(context.Request.Body);
                 string body = await reader.ReadToEndAsync();
                Dictionary<string,StringValues> queryDict =
                Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
                if (queryDict.ContainsKey("age"))
                {
                    string age = queryDict["age"][0]; // index = 0 because the value is a string and it allows same name for different values
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync($"<h3>age={age}<h3>");
                }
                /*or cycle through the dictionary
                foreach (var item in queryDict)
                {
                   ..
                }
                
                if (context.Request.Headers.ContainsKey("AuthenticationKey"))
                {
                    string aut = context.Request.Headers["AuthenticationKey"];
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync($"<h3>AuthenticationKey={aut}<h3>");
                }

                          
                                if (context.Request.Method == "GET")
                                {
                                    //DO SOMETHING

                                    if (context.Request.Query.ContainsKey("Id"))
                                    {
                                        context.Response.Headers["Content-Type"] = "text/html";

                                        string Id = context.Request.Query["Id"];
                                        await context.Response.WriteAsync($"idreceived={Id}");
                                    }
                                }

                           string path = context.Request.Path;
                           context.Response.Headers["Content-Type"] = "text/html";
                           await context.Response.WriteAsync($"<p>{path}</p>");

                               context.Response.Headers["Key"] = "Value here";
                               if (1 == 1)
                               {
                                   context.Response.StatusCode = 200;
                                   context.Response.Headers["Content-Type"] = "text/html";

                                   await context.Response.WriteAsync("<h3>yes it's one<h3>");
                                   await context.Response.WriteAsync("<h4>yes it's one<h4>");

                               }
                               else
                               {
                                   context.Response.StatusCode = 400;
                                   await context.Response.WriteAsync("invalid request");
                               }
                  */
                #endregion
            });
            app.Run();
        }
    }
}

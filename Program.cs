using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Primitives;
using NetCoreProyectExample.CustomMiddlewares;

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

            builder.Services.AddTransient<MyCustomMiddleware>();

            var app = builder.Build();//Build web app(config, default services etc)
                                      // .Build return a instance of object web app
                                      // app.MapGet("/", () => "Hi Future Developer!");

            //app run recibe un argumento  : httpcontext

            #region Routing (5)
            //enable routing
            app.UseRouting();
            //create end points
            app.UseEndpoints(endpoints =>
            {
                //add endpoints here

            } );

            #endregion


            #region HouseWork (4) Middlewares

            app.UseMiniLoginMiddleware();

            #endregion

            #region Middlewares
            /*
            recommended order to use middlewares
            Exception Handling
    
    HTTPS Redirection

    Static Files

    Routing

    CORS

    Authentication

    Authorization

    Custom Middleware

    MVC/Razor Pages/Minimal APIs



            */







         //   app.Map("/Hello", app.UseCustomMiddleware1); //

            //use  when allows conditions to execute middlewares
            app.UseWhen(
                context => context.Request.Query.ContainsKey("username"),
                app =>
                {
                    //execute only the condition (up) its true
                    app.Use(async (context, next) =>
                    {
                        await context.Response.WriteAsync("Hello from middleware branch\n");
                        await next(context);
                    });
                }
                );

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from the middleware main chain\n");
            });

            //we can able multiples app.run or app.use

            // Middleware 1
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("This middleware  ALLOW to forward to the next middleware\n");
                await next(context);
                await context.Response.WriteAsync("DO SOMETHING NEXT  A MIDDLEWARE TWO THAT FINALIZE\n");

            });


            //Middleware 2
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("This middleware ALLOW X2 to forward to the next middleware\n");
                await next(context);
                await context.Response.WriteAsync("DO SOMETHING NEXT  A MIDDLEWARE THREE THAT FINALIZE\n");

            });

            //Middleware 3 = CUSTOM CLASS
            // i use method extensions to invoque my middleware more easily
            app.UseCustomMiddleware1();
            app.UseClassCustomMiddleware();

            //Middleware 4

            app.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("This middleware DONT allow to forward to the next middleware\n");

            });
            #endregion
            /*
            app.Run(async (HttpContext context) =>
            {
            */


            #region HouseWork (3) Http
            /*
            // received 3 values. two numbers (int)  and a operation simbole  (+,-,*,/ or %)

            // values its part of a url
            context.Response.Headers["Content-Type"] = "text/html";
            string method = context.Request.Method;
            if (method == "GET")
            {
                if (context.Request.Query.ContainsKey("v1") && context.Request.Query.ContainsKey("v2") &&
                context.Request.Query.ContainsKey("ope"))
                {
                    int firstValue, secondValue;
                    string? operatorValue;
                    firstValue = int.Parse(context.Request.Query["v1"]);
                    secondValue = int.Parse(context.Request.Query["v2"]);
                    operatorValue = context.Request.Query["ope"];
                    decimal? finallyResult;
                    switch (operatorValue)
                    {
                        case "add":
                            finallyResult = firstValue + secondValue;
                            break;
                        case "less":
                            finallyResult = secondValue - firstValue;
                            break;
                        case "mul":
                            finallyResult = firstValue / secondValue;
                            break;
                        case "divide":
                            finallyResult = secondValue / secondValue;
                            break;
                        case "percentage":
                            finallyResult = firstValue / secondValue;
                            break;
                        default:
                            finallyResult = null;
                            break;

                    }
                    if(finallyResult == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid operation");
                        return;
                    }

                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Result = " + finallyResult);


                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Not all values are sending");
                }


            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Incorrect Method");
            }



            // finally return the result of the operation
            */
            #endregion


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
            /* 
            });
            */
            app.Run();
        }
    }
}

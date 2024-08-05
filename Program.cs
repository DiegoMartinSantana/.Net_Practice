using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using NetCoreProyectExample.CustomBinder;
using NetCoreProyectExample.CustomConstraints;
using NetCoreProyectExample.CustomMiddlewares;
using System.Diagnostics.Metrics;

namespace NetCoreProyectExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create builder for web app
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                WebRootPath = "myroot"
            }
                );
            //with that we can configure the web app or access to the services
            //builder.Configuration
            //
            //builder.Services

            //enable controllers
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.
            Insert(0, new CustomModelBinderPROVIDER() );
            });
            builder.Services.AddTransient<MyCustomMiddleware>();

            //add custom constraint 
            builder.Services.AddRouting(opt =>
            {
                opt.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
            });

            var app = builder.Build();//Build web app(config, default services etc)
                                      // .Build return a instance of object web app
                                      // app.MapGet("/", () => "Hi Future Developer!");

            //app run recibe un argumento  : httpcontext
            app.UseStaticFiles();
            #region Controllers (6)
            //enable map controllers
            app.MapControllers();
           

            #endregion


            #region HomeWork (5) Routing 
            //enable routing
            /*
               app.UseRouting();

               Dictionary<int, string> Countries = new Dictionary<int, string>();
               Countries.Add(1, "Usa");
               Countries.Add(2, "Canada");
               Countries.Add(3, "United Kingdom");
               Countries.Add(4, "India");
               Countries.Add(5, "Japan");


               Handler this dictionary with id request, all request and excepctions.

               //Path : /Countries



               //all
               app.MapGet("Countries", async context =>
               {
                   foreach (var country in Countries)
                   {
                       await context.Response.WriteAsync(" " + country.Value+ "");
                   }

               });

               //create a class to define the constraints

               //id
               app.MapGet("Countries/{id:int}", async context =>
               {
                   int id = Convert.ToInt32(context.Request.RouteValues["id"]);
                   if (id > 100 || id <0)
                   {
                       context.Response.StatusCode = 400;
                       await context.Response.WriteAsync("The value has not to be more at 100 at not less as 1");
                       return;
                   }
                   if (!Countries.ContainsKey(id))
                   {
                       context.Response.StatusCode = 404;
                       await context.Response.WriteAsync("The value dont exists in the Bd.");
                       return;
                   }

                   context.Response.StatusCode = 200;
                   await context.Response.WriteAsync("Country : " + Countries[id]);

               });
               */

            #endregion


            #region Routing (5)
            /*
            //create end points

            //work with "myroot"
            app.UseStaticFiles();

            //work with another root 
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "myotherwebroot"))
            });


            app.Map("/", async context =>
            {
                await context.Response.WriteAsync("hello");
            });
            //AFTER USE ROUTING. CAN SAVE THE ENDPOINT
            Endpoint? endpoint = null;
            app.Use(async (context, next) =>
            {
                endpoint = context.GetEndpoint();

                await next();
            });

            //CAN ADD PARAMETERS TO THE PATH
            //default parameter
            app.MapGet("sellers/{country=argentina}", async (context) =>
            {
                string? country = Convert.ToString(context.Request.RouteValues["country"]);
                await context.Response.WriteAsync("country = " + country);
            });
            //optional parameteres 
            app.MapGet("buyers/{country?}", async (context) =>
            {
                if (context.Request.RouteValues.ContainsKey("country"))
                {
                    string? country = Convert.ToString(context.Request.RouteValues["country"]);
                    await context.Response.WriteAsync("buyers = " + country);
                }
                else
                {
                    await context.Response.WriteAsync("parameter has not been passed");

                }
            });
            //PARAMETER WITH CONSTRAINTS
            app.MapGet("sellers33/{name:length(3,20)=pepe}", async context =>
            {
                string? name = Convert.ToString(context.Request.RouteValues["name"]);
                await context.Response.WriteAsync($"name = {name}");

            });
            app.MapGet("sale/{year:int:min(2022)}/{month:regex(^(apr|jul|oct)$)}", async context =>
            {
                int valueyear = Convert.ToInt32(context.Request.RouteValues["year"]);
                string? valuemonth = Convert.ToString(context.Request.RouteValues["month"]);
                await context.Response.WriteAsync("the parameter its a date : " + valueyear + "month =" + valuemonth);

            });
            //but the best way is accept wrong values and response basement in that
            app.MapGet("sale2/{year=2024}/{month=jul}", async context =>
            {
                int valueyear = Convert.ToInt32(context.Request.RouteValues["year"]);
                string? valuemonth = Convert.ToString(context.Request.RouteValues["month"]);
                if (valueyear > 2022 && valuemonth == "apr" || valuemonth == "jul" || valuemonth == "oct")
                {
                    await context.Response.WriteAsync("the parameter its a date : " + valueyear + "month =" + valuemonth);
                }
                else
                {
                    await context.Response.WriteAsync("invalid values");

                }
            });
            app.MapGet("sale2/{year=2024}/{month:months}", async context =>
{
    int valueyear = Convert.ToInt32(context.Request.RouteValues["year"]);
    string valuemonth = Convert.ToString(context.Request.RouteValues["month"]);
    if (valueyear > 2022 && (valuemonth == "apr" || valuemonth == "jul" || valuemonth == "oct"))
    {
        await context.Response.WriteAsync("The parameter is a date: " + valueyear + " month = " + valuemonth);
    }
    else
    {
        await context.Response.WriteAsync("Invalid values");
    }
});


            app.MapGet("sellers2/{name:int}", async context =>
            {
                int? value = Convert.ToInt32(context.Request.RouteValues["name"]);
                await context.Response.WriteAsync("the parameter its a int  : " + value);

            });
            app.MapGet("sellersGuid/{id:guid}", async context =>
            {
                Guid? value = Guid.Parse(Convert.ToString(context.Request.RouteValues["id"]));
                await context.Response.WriteAsync("guid value  : " + value);

            });
            app.MapGet("files/{filename}.{extension}", async (context) =>
            {
                string? file = Convert.ToString(context.Request.RouteValues["filename"]);
                string? ext = Convert.ToString(context.Request.RouteValues["extension"]);
                await context.Response.WriteAsync($"filename = {file} and extension is {ext}");
            });


            app.UseEndpoints(endpoints =>
            {
                //add endpoints here
                endpoints.Map("mapExample", async (context) =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.Headers["Content-Type"] = "text/html";
                    await context.Response.WriteAsync("in map example");
                });
                endpoints.Map("mapExample2", async (context) =>
                {
                    await context.Response.WriteAsync("in map example2");
                    context.Response.StatusCode = 200;

                });

                app.MapGet("mapExample", async (context) =>
                {
                    context.Response.StatusCode = 200;

                    await context.Response.WriteAsync("in getmapExample");
                });
            });

            //important : 
            app.MapGet("mapExample4", async (context) =>
            {
                await context.Response.WriteAsync("in getmapExample WITHOUT ENDPOINTS");

            });


            //if the url dont match execute that:
            app.Run(async (context) =>
            {
                context.Response.Headers["Content-Type"] = "text/html";

                string path = context.Request.Path;
                await context.Response.WriteAsync("<h1>the path is : " + path + "</h1>");


            });
            */
            #endregion


            #region HomeWork (4) Middlewares
            /*
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
            */
            #endregion
            /*
            app.Run(async (HttpContext context) =>
            {
            */


            #region HomeWork (3) Http
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

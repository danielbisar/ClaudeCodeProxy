using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.Http.Diagnostics;
using Microsoft.Net.Http.Headers;

using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
        .AddTransforms(context =>
            {
                context.AddRequestTransform(async rt =>
                    {
                        var request = rt.ProxyRequest;
                        Console.WriteLine("==== request ====");
                        Console.WriteLine(request.Method);
                        Console.WriteLine(request);
                        Console.WriteLine(await request.Content?.ReadAsStringAsync());
                    }
                );

                context.AddResponseTransform(async rt =>
                    {
                        var response = rt.ProxyResponse;
                        Console.WriteLine("==== response ====");
                        Console.WriteLine(response);
                        Console.WriteLine(await response?.Content?.ReadAsStringAsync());
                    }
                );
            }
        );


var app = builder.Build();
// app.UseHttpLogging();
app.UseRouting();
app.MapReverseProxy();

app.Run();

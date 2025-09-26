using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.Http.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<IRedactorProvider, NullRedactorProvider>();
services.AddHttpLogging(o =>
    {
        o.LoggingFields = HttpLoggingFields.All;
        // o.RequestHeaders.Add("x-ms-client-request-id");
        // o.ResponseHeaders.Add("x-ms-request-id");
        // o.ResponseHeaders.Add("x-ms-correlation-request-id");
        o.RequestBodyLogLimit = 4096 * 4096;
        o.ResponseBodyLogLimit = 4096 * 4096;
    }
);
services.AddHttpLoggingRedaction(options =>
{
    options.RequestHeadersDataClasses.Clear();
    options.ResponseHeadersDataClasses.Clear();
    options.RouteParameterDataClasses.Clear();
    options.RequestPathParameterRedactionMode = HttpRouteParameterRedactionMode.None;
});

services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();
app.UseHttpLogging();
app.UseRouting();
app.MapReverseProxy();

app.Run();

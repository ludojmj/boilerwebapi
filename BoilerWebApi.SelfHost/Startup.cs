using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using BoilerWebApi.Shared;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(BoilerWebApi.SelfHost.Startup))]
namespace BoilerWebApi.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Swagger
            httpConfiguration
                .EnableSwagger(c => c.SingleApiVersion("v1", "BoilerWebApi"))
                .EnableSwaggerUi();

            // App Insights
            // TelemetryConfiguration.Active.DisableTelemetry = DisableTelemetry;
            // TelemetryConfiguration.Active.InstrumentationKey = InstrumentationKey;

            // Exceptions handling
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            
            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Json only
            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

            app.UseWebApi(httpConfiguration);

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using BoilerWebApi.Shared;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
#if DEBUG
using Swashbuckle.Application;
#endif

[assembly: OwinStartup(typeof(BoilerWebApi.Startup))]
namespace BoilerWebApi
{
    /// <summary>
    /// Web API configuration and services.
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            var httpConfiguration = new HttpConfiguration();
#if DEBUG
            // Swagger
            httpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "BoilerWebApi");
                    c.RootUrl(req => new Uri(req.RequestUri, req.GetRequestContext().VirtualPathRoot).ToString());
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi();
#endif
            // Configure Log4Net
            log4net.Config.XmlConfigurator.Configure();

            // Exceptions handling
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Exceptions logging
            httpConfiguration.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
            // Requests/Responses tracing
            httpConfiguration.MessageHandlers.Add(new GlobalTraceHandler());


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

            // Add Json mapping
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".json"] = JsonMediaTypeFormatter.DefaultMediaType.MediaType;

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },
                FileSystem = new PhysicalFileSystem("./public"),
                RequestPath = new PathString(string.Empty),
                StaticFileOptions = { ContentTypeProvider = provider }
            });

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Http.ExceptionHandling;

// using Microsoft.ApplicationInsights;

namespace BoilerWebApi.Shared
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        // private readonly TelemetryClient _telemetry = new TelemetryClient();

        public override void Handle(ExceptionHandlerContext context)
        {
            var aggregateException = context.Exception as AggregateException;
            string msg = context.Exception.Message;

            if (aggregateException != null)
            {
                aggregateException.Handle(ex =>
                {
                    // FaultException and TimeoutException.
                    msg = ex.Message;
                    return true;
                });
            }
            // Default Message.
            var compilationSection = ConfigurationManager.GetSection(@"system.web/compilation") as CompilationSection;
            if (compilationSection != null && !compilationSection.Debug)
            {
                // Fake message if Release.
                msg = "An error occured. Please try again later";
            }

            var businessException = context.Exception as BusinessException;
            if (businessException == null || businessException.Message.Contains("BusinessException"))
            {
                // We won't show the real exception to the user but we're going to keep a trace.
                // _telemetry.TrackException(context.Exception);
            }
            else
            {
                // Don't need to trace managed errors.
                msg = businessException.Message;
            }
            msg = Regex.Replace(msg, @"\s+", " ").Replace("\r", " ").Replace("\n", "").Replace("\"", "");
            context.Result = new ConflictActionResult(msg);
        }
    }
}
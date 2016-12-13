using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Http.ExceptionHandling;

namespace BoilerWebApi.Shared
{
    /// <summary>
    /// Intercept exceptions so as to avoid us to try catch everywhere.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
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
                // Fake message if Release (we won't show the real exception message to the user).
                msg = "An error occured. Please try again later";
            }

            var businessException = context.Exception as BusinessException;
            if (businessException != null)
            {
                // BusinessException = Managed exception (we can show the exception message to the user).
                msg = businessException.Message;
            }
            msg = Regex.Replace(msg, @"\s+", " ").Replace("\r", " ").Replace("\n", "").Replace("\"", "");
            context.Result = new ConflictActionResult(msg);
        }
    }
}
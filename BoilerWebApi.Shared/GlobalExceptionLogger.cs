using System.Web.Http.ExceptionHandling;
using log4net;

namespace BoilerWebApi.Shared
{
    /// <summary>
    ///  Unmanaged Exceptions Logging (for bugs)
    /// </summary>
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logg = LogManager.GetLogger("Errors");

        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception as BusinessException;
            if (exception == null)
            {
                Logg.Fatal(context.Exception);
            }
        }
    }
}

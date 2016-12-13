using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.Owin;

namespace BoilerWebApi.Shared
{
    /// <summary>
    /// Intercept operations so as to log requests and responses.
    /// </summary>
    public class GlobalTraceHandler : DelegatingHandler
    {
        private static readonly ILog Logg = LogManager.GetLogger("Traces");

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Log Parameters
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var guid = Guid.NewGuid();
            ThreadContext.Properties["Guid"] = guid;

            var wacLogin = GetClientIp(request);

            // Request trace input
            var traceIn = string.Format("00:00:00.000000;User: {0};Q: {1} {2}", wacLogin, request.Method, request.RequestUri);
            Logg.Info(traceIn);

            try
            {
                // Trying requested operation
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
                Tracing(stopWatch, wacLogin, "OK");
                return response;
            }
            catch (Exception)
            {
                Tracing(stopWatch, wacLogin, "KO");
                throw;
            }
        }

        private static void Tracing(Stopwatch stopWatch, string wacLogin, string status)
        {
            // Timer
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{00:00}:{1:00}:{2:00}.{3:000000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            // Response trace ouput
            var traceOut = string.Format("{0};User: {1};R: {2}", elapsedTime, wacLogin, status);
            Logg.Info(traceOut);
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                return ((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
            }
            return null;
        }
    }
}

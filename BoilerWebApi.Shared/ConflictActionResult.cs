using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BoilerWebApi.Shared
{
    public class ConflictActionResult : IHttpActionResult
    {
        private readonly string _message;

        public ConflictActionResult(string message)
        {
            _message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent(_message),
                ReasonPhrase = _message
            };
            return Task.FromResult(response);
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports
{
    // This is a workaround for stuff like this:
    // http://stackoverflow.com/questions/22157596/asp-net-web-api-operationcanceledexception-when-browser-cancels-the-request
    public class CancelledTaskBugWorkaroundMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request,
            CancellationToken cancellation_token )
        {
            var response = await base.SendAsync( request, cancellation_token );

            // Try to suppress response content when the cancellation token has fired; ASP.NET will log to the Application event log if there's content in this case.
            if ( cancellation_token.IsCancellationRequested )
            {
                return new HttpResponseMessage( HttpStatusCode.InternalServerError );
            }

            return response;
        }
    }
}
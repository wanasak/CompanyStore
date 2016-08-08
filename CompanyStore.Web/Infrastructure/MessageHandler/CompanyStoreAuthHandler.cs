using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Infrastructure.MessageHandler
{
    public class CompanyStoreAuthHandler : DelegatingHandler
    {
        IEnumerable<string> authHeadersValues = null;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out authHeadersValues);
                if (authHeadersValues == null)
                    return base.SendAsync(request, cancellationToken);
                var tokens = authHeadersValues.FirstOrDefault();
                tokens = tokens.Replace("Basic", "").Trim();
                if (!string.IsNullOrEmpty(tokens))
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedString.Split(':');
                    //var membershipService = request.GetMembershipService();
                    //var membershipContext = membershipContext.
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }
    }
}

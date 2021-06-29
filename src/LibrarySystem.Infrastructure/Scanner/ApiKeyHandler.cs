using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Scanner
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly InfrastructureOptions _opts;

        public ApiKeyHandler(InfrastructureOptions opts)
        {
            _opts = opts;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var apiKeyHeader = _opts.ApiKey + ":" + _opts.ApiSecret;
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKeyHeader)));
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

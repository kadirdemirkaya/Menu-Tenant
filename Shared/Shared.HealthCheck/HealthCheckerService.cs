using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Shared.HealthCheck
{
    public class HealthChecker(IHttpClientFactory httpClientFactory, string clientName) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var client = httpClientFactory.CreateClient(clientName);
            var response = await client.GetAsync("/health", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }

    public class HealthChecker2(string url) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                HttpResponseMessage response = await client.GetAsync("/health");

                if (response.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy();
                else
                    return HealthCheckResult.Unhealthy();
            }
        }
    }
}

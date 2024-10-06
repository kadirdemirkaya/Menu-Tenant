using Polly;
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.Services
{
    public class PollyPolicyService : IPollyPolicyService
    {
        public Policy RetryPolicy { get; private set; }
        public AsyncPolicy CircuitBreakerPolicy { get; private set; }
        public Policy TimeoutPolicy { get; private set; }
        public AsyncPolicy BulkheadPolicy { get; private set; }
        public Policy FallbackPolicy { get; private set; }

        public PollyPolicyService()
        {
            // Retry Policy - 5 kere dene, her denemede 5 saniye bekle
            RetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Error: {exception.Message}. Retry attempt: {retryCount}");
                });

            // Circuit Breaker Policy - 2 hata aldıktan sonra devreyi kes, 10 saniye sonra tekrar dene
            CircuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10),
                    onBreak: (ex, breakDelay) =>
                    {
                        Console.WriteLine($"Circuit broken! Delay: {breakDelay.TotalSeconds} seconds");
                    },
                    onReset: () => Console.WriteLine("Circuit reset!"),
                    onHalfOpen: () => Console.WriteLine("Circuit half-open, testing next action."));

            // Timeout Policy - 3 saniye sonra işlem zaman aşımına uğrasın
            TimeoutPolicy = Policy
                .Timeout(3, Polly.Timeout.TimeoutStrategy.Pessimistic, (context, timeSpan, task) =>
                {
                    Console.WriteLine($"Operation timed out after {timeSpan.TotalSeconds} seconds.");
                });

            // Bulkhead Policy - Aynı anda en fazla 5 işleme izin ver, 10 bekleme kuyruğuna izin ver
            BulkheadPolicy = Policy.BulkheadAsync(5, 10, context =>
            {
                Console.WriteLine("Bulkhead limit reached, too many concurrent operations.");
                return Task.CompletedTask;
            });

            // Fallback Policy - Hata oluşursa alternatif işlem yap
            FallbackPolicy = Policy
                .Handle<Exception>()
                .Fallback(() =>
                {
                    Console.WriteLine("Fallback executed, providing default value.");
                });
        }
    }
}

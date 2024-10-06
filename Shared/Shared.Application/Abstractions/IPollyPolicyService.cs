using Polly;

namespace Shared.Application.Abstractions
{
    public interface IPollyPolicyService
    {
        Policy RetryPolicy { get; }
        AsyncPolicy CircuitBreakerPolicy { get; }
        Policy TimeoutPolicy { get; }
        AsyncPolicy BulkheadPolicy { get; }
        Policy FallbackPolicy { get; }
    }
}

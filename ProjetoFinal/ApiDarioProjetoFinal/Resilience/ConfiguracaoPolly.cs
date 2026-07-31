using Polly;
using Polly.Extensions.Http;

namespace ApiDarioProjetoFinal.Resilience
{
    public static class ConfiguracaoPolly
    {
        public static IAsyncPolicy<HttpResponseMessage> CriarPoliticaRetry()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    3,
                    tentativa => TimeSpan.FromSeconds(tentativa)
                );
        }


        public static IAsyncPolicy<HttpResponseMessage> CriarCircuitBreaker()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    3,
                    TimeSpan.FromSeconds(30)
                );
        }

        public static IAsyncPolicy<HttpResponseMessage> CriarFallback()
        {
            return Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"estado\":\"Pagamento temporariamente indisponível\"}"
                    )
                });
        }
    }
}
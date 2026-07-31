using System.Net.Http.Json;

namespace ApiDarioProjetoFinal.External
{
    public class ServicoPagamentoExterno
    {
        private readonly HttpClient _httpClient;

        public ServicoPagamentoExterno(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("PagamentoAPI");
        }

        public async Task<bool> ProcessarPagamento(decimal valor)
        {
            var resposta = await _httpClient.PostAsJsonAsync(
                "http://localhost:4545/payments",
                new
                {
                    valor = valor
                });

            return resposta.IsSuccessStatusCode;
        }
    }
}
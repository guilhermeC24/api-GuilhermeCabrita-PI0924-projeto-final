using ApiDarioProjetoFinal.External;
using Microsoft.AspNetCore.Mvc;

namespace ApiDarioProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly ServicoPagamentoExterno _servicoPagamento;

        public PagamentosController(ServicoPagamentoExterno servicoPagamento)
        {
            _servicoPagamento = servicoPagamento;
        }

        [HttpPost]
        public async Task<IActionResult> Processar(decimal valor)
        {
            var resultado = await _servicoPagamento.ProcessarPagamento(valor);

            if (resultado)
            {
                return Ok(new
                {
                    estado = "Pagamento aprovado"
                });
            }

            return BadRequest(new
            {
                estado = "Pagamento recusado"
            });
        }
    }
}
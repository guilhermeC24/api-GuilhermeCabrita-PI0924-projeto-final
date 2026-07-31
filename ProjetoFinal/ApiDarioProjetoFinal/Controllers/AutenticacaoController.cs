using ApiDarioProjetoFinal.Data;
using ApiDarioProjetoFinal.DTOs;
using ApiDarioProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDarioProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ServicoJwt _servicoJwt;

        public AutenticacaoController()
        {
            _servicoJwt = new ServicoJwt();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginPedido pedido)
        {
            var utilizador = ArmazenamentoDados.Utilizadores
                .FirstOrDefault(u =>
                    u.Email == pedido.Email &&
                    u.Password == pedido.Password);

            if (utilizador == null)
                return Unauthorized("Email ou password incorretos");

            var token = _servicoJwt.CriarToken(
                utilizador.Email,
                utilizador.Perfil
            );

            return Ok(new
            {
                token = token
            });
        }
    }
}
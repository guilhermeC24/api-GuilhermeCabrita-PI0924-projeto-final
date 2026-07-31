using ApiDarioProjetoFinal.Cache;
using ApiDarioProjetoFinal.Data;
using ApiDarioProjetoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDarioProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservasController : ControllerBase
    {
        private readonly CacheServico _cache;

        public ReservasController(CacheServico cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult ObterTodas()
        {
            var reservasCache = _cache.Obter<List<Reserva>>("reservas");

            if (reservasCache != null)
            {
                return Ok(reservasCache);
            }

            var reservas = ArmazenamentoDados.Reservas;

            _cache.Guardar("reservas", reservas);

            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            string chave = $"reserva_{id}";

            var reservaCache = _cache.Obter<Reserva>(chave);

            if (reservaCache != null)
            {
                return Ok(reservaCache);
            }

            var reserva = ArmazenamentoDados.Reservas
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            _cache.Guardar(chave, reserva);

            return Ok(reserva);
        }

        [HttpPost]
        public IActionResult Criar(Reserva reserva)
        {
            ArmazenamentoDados.Reservas.Add(reserva);

            _cache.Guardar("reservas", ArmazenamentoDados.Reservas);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = reserva.Id },
                reserva
            );
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Reserva reservaAtualizada)
        {
            var reserva = ArmazenamentoDados.Reservas
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            reserva.IdFilme = reservaAtualizada.IdFilme;
            reserva.IdUtilizador = reservaAtualizada.IdUtilizador;
            reserva.QuantidadeBilhetes = reservaAtualizada.QuantidadeBilhetes;
            reserva.EstadoPagamento = reservaAtualizada.EstadoPagamento;

            _cache.Guardar($"reserva_{id}", reserva);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var reserva = ArmazenamentoDados.Reservas
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            ArmazenamentoDados.Reservas.Remove(reserva);

            _cache.Guardar("reservas", ArmazenamentoDados.Reservas);

            return NoContent();
        }
    }
}
using ApiDarioProjetoFinal.Cache;
using ApiDarioProjetoFinal.Data;
using ApiDarioProjetoFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDarioProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilizadoresController : ControllerBase
    {
        private readonly CacheServico _cache;

        public UtilizadoresController(CacheServico cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var utilizadoresCache = _cache.Obter<List<Utilizador>>("utilizadores");

            if (utilizadoresCache != null)
            {
                return Ok(utilizadoresCache);
            }

            var utilizadores = ArmazenamentoDados.Utilizadores;

            _cache.Guardar("utilizadores", utilizadores);

            return Ok(utilizadores);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            string chave = $"utilizador_{id}";

            var utilizadorCache = _cache.Obter<Utilizador>(chave);

            if (utilizadorCache != null)
            {
                return Ok(utilizadorCache);
            }

            var utilizador = ArmazenamentoDados.Utilizadores
                .FirstOrDefault(u => u.Id == id);

            if (utilizador == null)
            {
                return NotFound();
            }

            _cache.Guardar(chave, utilizador);

            return Ok(utilizador);
        }

        [HttpPost]
        public IActionResult Criar(Utilizador utilizador)
        {
            ArmazenamentoDados.Utilizadores.Add(utilizador);

            _cache.Guardar("utilizadores", ArmazenamentoDados.Utilizadores);

            return CreatedAtAction(nameof(ObterPorId), new { id = utilizador.Id }, utilizador);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Utilizador utilizadorAtualizado)
        {
            var utilizador = ArmazenamentoDados.Utilizadores
                .FirstOrDefault(u => u.Id == id);

            if (utilizador == null)
            {
                return NotFound();
            }

            utilizador.Nome = utilizadorAtualizado.Nome;
            utilizador.Email = utilizadorAtualizado.Email;
            utilizador.Password = utilizadorAtualizado.Password;
            utilizador.Perfil = utilizadorAtualizado.Perfil;

            _cache.Guardar($"utilizador_{id}", utilizador);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var utilizador = ArmazenamentoDados.Utilizadores
                .FirstOrDefault(u => u.Id == id);

            if (utilizador == null)
            {
                return NotFound();
            }

            ArmazenamentoDados.Utilizadores.Remove(utilizador);

            _cache.Guardar("utilizadores", ArmazenamentoDados.Utilizadores);

            return NoContent();
        }
    }
}
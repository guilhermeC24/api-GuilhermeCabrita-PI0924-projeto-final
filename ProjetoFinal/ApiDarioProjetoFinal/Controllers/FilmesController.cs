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
    public class FilmesController : ControllerBase
    {
        private readonly CacheServico _cache;

        public FilmesController(CacheServico cache)
        {
            _cache = cache;
        }


        [HttpGet]
        public IActionResult ObterTodos()
        {
            var filmesCache = _cache.Obter<List<Filme>>("filmes");

            if (filmesCache != null)
            {
                return Ok(filmesCache);
            }


            var filmes = ArmazenamentoDados.Filmes;

            _cache.Guardar("filmes", filmes);

            return Ok(filmes);
        }


        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            string chave = $"filme_{id}";

            var filmeCache = _cache.Obter<Filme>(chave);

            if (filmeCache != null)
            {
                return Ok(filmeCache);
            }


            var filme = ArmazenamentoDados.Filmes
                .FirstOrDefault(f => f.Id == id);


            if (filme == null)
            {
                return NotFound();
            }


            _cache.Guardar(chave, filme);

            return Ok(filme);
        }


        [HttpPost]
        public IActionResult Criar(Filme filme)
        {
            ArmazenamentoDados.Filmes.Add(filme);

            // limpa cache para atualizar a lista
            _cache.Guardar("filmes", ArmazenamentoDados.Filmes);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = filme.Id },
                filme
            );
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Filme filmeAtualizado)
        {
            var filme = ArmazenamentoDados.Filmes
                .FirstOrDefault(f => f.Id == id);


            if (filme == null)
            {
                return NotFound();
            }


            filme.Titulo = filmeAtualizado.Titulo;
            filme.Genero = filmeAtualizado.Genero;
            filme.Duracao = filmeAtualizado.Duracao;
            filme.ClassificacaoEtaria = filmeAtualizado.ClassificacaoEtaria;
            filme.PrecoBilhete = filmeAtualizado.PrecoBilhete;


            _cache.Guardar($"filme_{id}", filme);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var filme = ArmazenamentoDados.Filmes
                .FirstOrDefault(f => f.Id == id);


            if (filme == null)
            {
                return NotFound();
            }


            ArmazenamentoDados.Filmes.Remove(filme);


            _cache.Guardar("filmes", ArmazenamentoDados.Filmes);

            return NoContent();
        }
    }
}
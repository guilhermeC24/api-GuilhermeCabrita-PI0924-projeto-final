using Microsoft.Extensions.Caching.Memory;

namespace ApiDarioProjetoFinal.Cache
{
    public class CacheServico
    {
        private readonly IMemoryCache _cache;

        public CacheServico(IMemoryCache cache)
        {
            _cache = cache;
        }


        public void Guardar<T>(string chave, T dados)
        {
            _cache.Set(
                chave,
                dados,
                TimeSpan.FromMinutes(5)
            );
        }


        public T? Obter<T>(string chave)
        {
            _cache.TryGetValue(chave, out T? dados);

            return dados;
        }
    }
}
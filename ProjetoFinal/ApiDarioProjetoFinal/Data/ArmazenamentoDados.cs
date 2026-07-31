using ApiDarioProjetoFinal.Models;

namespace ApiDarioProjetoFinal.Data
{
    public static class ArmazenamentoDados
    {
        public static List<Filme> Filmes { get; } = new()
        {
            new Filme
            {
                Id = 1,
                Titulo = "Avatar",
                Genero = "Ficção Científica",
                Duracao = 162,
                ClassificacaoEtaria = "M/12",
                PrecoBilhete = 8.50m
            },
            new Filme
            {
                Id = 2,
                Titulo = "Deadpool",
                Genero = "Ação",
                Duracao = 128,
                ClassificacaoEtaria = "M/16",
                PrecoBilhete = 7.50m
            }
        };

        public static List<Utilizador> Utilizadores { get; } = new()
        {
            new Utilizador
            {
                Id = 1,
                Nome = "Administrador",
                Email = "admin@cinema.pt",
                Password = "123456",
                Perfil = "Administrador"
            },
            new Utilizador
            {
                Id = 2,
                Nome = "João Silva",
                Email = "joao@cinema.pt",
                Password = "123456",
                Perfil = "Cliente"
            }
        };

        public static List<Reserva> Reservas { get; } = new()
        {
            new Reserva
            {
                Id = 1,
                IdFilme = 1,
                IdUtilizador = 2,
                QuantidadeBilhetes = 2,
            EstadoPagamento = "Pago"
            },
            new Reserva
            {
                Id = 2,
                IdFilme = 2,
                IdUtilizador = 2,
                QuantidadeBilhetes = 3,
                EstadoPagamento = "Pendente"
            }
        };
    }
}
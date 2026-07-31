namespace ApiDarioProjetoFinal.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public int IdUtilizador { get; set; }
        public int QuantidadeBilhetes { get; set; }
        public string EstadoPagamento { get; set; } = "Pendente";
    }
}
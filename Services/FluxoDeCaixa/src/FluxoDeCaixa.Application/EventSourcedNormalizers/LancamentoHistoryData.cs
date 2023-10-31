namespace FluxoDeCaixa.Application.EventSourcedNormalizers
{
    public class LancamentoHistoryData
    {
        public string Id { get; set; }
        public string IdUsuario { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
        public string Timestamp { get; set; }
        public string Action { get; set; }
    }
}
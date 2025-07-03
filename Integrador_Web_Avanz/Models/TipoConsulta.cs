namespace Integrador_Web_Avanz.Models
{
    public class TipoConsulta
    {
        public int IdTipoConsulta {  get; set; }

        public string? TipoDeConsulta { get; set; }

        public decimal? Precio {  get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
    }
}

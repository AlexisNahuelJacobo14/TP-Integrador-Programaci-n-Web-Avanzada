namespace Integrador_Web_Avanz.Models
{
	public class ClienteConsultaVM
	{
		public Consulta Consulta { get; set; } = new Consulta();
		public Cliente Cliente { get; set; } = new Cliente();

		//public List<Partner> _listaPartners { get; set; }
	}
}

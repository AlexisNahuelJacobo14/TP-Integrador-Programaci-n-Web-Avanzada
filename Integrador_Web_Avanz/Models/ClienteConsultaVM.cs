using Microsoft.AspNetCore.Mvc.Rendering;

namespace Integrador_Web_Avanz.Models
{
	public class ClienteConsultaVM
	{
		public Consulta Consulta { get; set; } = new Consulta();
		public Cliente Cliente { get; set; } = new Cliente();

		public List<SelectListItem> _listaPartners { get; set; } = new List<SelectListItem>();
	}
}

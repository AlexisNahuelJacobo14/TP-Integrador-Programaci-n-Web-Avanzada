using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integrador_Web_Avanz.Models;

public partial class Consulta
{
    public int IdConsulta { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe ser entre 1 y 50")]
	[Display(Name = "Tipo de la consulta")]

	public string? TipoConsulta { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCliente { get; set; }

	public int? IdPartner { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

	public virtual Partner? IdPartnerNavigation { get; set; }
}

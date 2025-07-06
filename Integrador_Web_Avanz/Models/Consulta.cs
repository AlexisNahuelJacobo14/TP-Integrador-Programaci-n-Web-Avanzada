using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integrador_Web_Avanz.Models;

public partial class Consulta
{
    public int IdConsulta { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido, cuentenos brevemente en qué lo podemos ayudar.")]
	[Display(Name = "Descripción")]

	public string? Descripcion { get; set; }

    public int? IdCliente { get; set; }  // FK

    public int? IdPartner { get; set; }  // FK

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[Display(Name = "Tipo de la consulta")]
	public int? IdTipoConsulta { get; set; }  // FK
    public virtual TipoConsulta? IdTipoConsultaNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

	public virtual Partner? IdPartnerNavigation { get; set; }
}

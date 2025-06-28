using System;
using System.Collections.Generic;

namespace Integrador_Web_Avanz.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public partial class Consulta
{
    public int IdConsulta { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [StringLength(maximumLength:50, MinimumLength = 5, ErrorMessage ="La longitud del campo debe ser entre 5 y 50")]
    [Display(Name="Tipo de la consulta")]
    public string? TipoConsulta { get; set; }


	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe ser entre 1 y 50")]
	[Display(Name = "Descripción de la consulta")]
	public string? Descripcion { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? _Cliente { get; set; }

}

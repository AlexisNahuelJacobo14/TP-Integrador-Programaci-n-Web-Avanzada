using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integrador_Web_Avanz.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

	/*[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo debe ser entre 3 y 50")]
	[Display(Name = "Nombre")]*/

	public string? Nombre { get; set; }

    public string? Apellido { get; set; }

	/*[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "La longitud del campo debe ser entre 5 y 50")]
	[EmailAddress(ErrorMessage = "El correo debe tener un formato válido")]*/


	public string? Mail { get; set; }

	public string? Telefono { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}

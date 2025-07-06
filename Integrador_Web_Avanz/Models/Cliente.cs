using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Integrador_Web_Avanz.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo debe ser entre 3 y 50")]
	[Display(Name = "Nombre")]

	public string? Nombre { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo debe ser entre 3 y 50")]
	[Display(Name = "Apellido")]
	public string? Apellido { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[Range(1, 100000000, ErrorMessage = "El campo DNI es requerido.")]
	[Display(Name = "DNI")]
	public int Dni {  get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[EmailAddress(ErrorMessage = "El correo debe tener un formato válido")]

	public string? Mail { get; set; }

	public string? Telefono { get; set; }

	[JsonIgnore]
	public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}

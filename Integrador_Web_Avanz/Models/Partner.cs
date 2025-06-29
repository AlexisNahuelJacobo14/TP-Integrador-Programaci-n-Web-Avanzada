using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integrador_Web_Avanz.Models;

public partial class Partner
{
	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe ser entre 1 y 50")]
	[Display(Name = "Partner")]
	public int IdPartner { get; set; }

	[Required(ErrorMessage = "El campo {0} es requerido.")]
	[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe ser entre 1 y 50")]
	[Display(Name = "Partner")]

	public string? Marca { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public Partner() { }
    
    public Partner(string? marca, string? imagen)
	{
		Marca = marca;
		Imagen = imagen;	
	}
}

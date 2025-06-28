using System;
using System.Collections.Generic;

namespace Integrador_Web_Avanz.Models;

public partial class Partner
{
    public int IdPartner { get; set; }

    public string? Marca { get; set; }

    public string? Imagen { get; set; }

    public Partner(string? marca, string? imagen)
	{ 
		this.Marca = marca;
		this.Imagen = imagen;
	}
}


using System;
using System.Collections.Generic;

namespace Integrador_Web_Avanz.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Mail { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}





using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Colonium
{
    public int IdColonia { get; set; }

    public string? Nombre { get; set; }

    public string? Municipio { get; set; }

    public string? Departamento { get; set; }

    public virtual ICollection<ClienteDireeccion> ClienteDireeccions { get; } = new List<ClienteDireeccion>();
}

using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int CodigoPersonal { get; set; }

    public int Dpi { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public int Telefono { get; set; }

    public string Email { get; set; } = null!;

    public sbyte Estado { get; set; }

    public virtual ICollection<ClienteDireeccion> ClienteDireeccions { get; } = new List<ClienteDireeccion>();

    public virtual ICollection<Factura> Facturas { get; } = new List<Factura>();
}

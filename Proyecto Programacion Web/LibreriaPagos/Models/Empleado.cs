using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int CodigoPersonal { get; set; }

    public int Dpi { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public int Telefono { get; set; }

    public string Email { get; set; } = null!;

    public sbyte Estado { get; set; }

    public int RolIdRol { get; set; }

    public virtual ICollection<DireccionEmpleado> DireccionEmpleados { get; } = new List<DireccionEmpleado>();

    public virtual ICollection<Facturaempleado> Facturaempleados { get; } = new List<Facturaempleado>();

    public virtual ICollection<Factura> Facturas { get; } = new List<Factura>();

    public virtual Rol RolIdRolNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}

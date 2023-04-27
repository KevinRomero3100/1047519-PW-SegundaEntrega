using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}

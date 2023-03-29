using System;
using System.Collections.Generic;

namespace ApiPagos.EntityModels;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreDeUsuario { get; set; } = null!;

    public string Pasword { get; set; } = null!;

    public int EmpleadoIdEmpleado { get; set; }

    public virtual Empleado EmpleadoIdEmpleadoNavigation { get; set; } = null!;
}

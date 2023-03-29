using System;
using System.Collections.Generic;

namespace ApiPagos.EntityModels;

public partial class DireccionEmpleado
{
    public int IdDireccionEmpleado { get; set; }

    public int EmpleadoIdEmpleado { get; set; }

    public int DireccionIdDireccion { get; set; }

    public virtual Direccion DireccionIdDireccionNavigation { get; set; } = null!;

    public virtual Empleado EmpleadoIdEmpleadoNavigation { get; set; } = null!;
}

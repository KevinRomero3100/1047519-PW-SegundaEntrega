using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Facturaempleado
{
    public int IdFacturaEmpleado { get; set; }

    public int FacturaIdFactura { get; set; }

    public int EmpleadoIdEmpleado { get; set; }

    public virtual Empleado EmpleadoIdEmpleadoNavigation { get; set; } = null!;

    public virtual Factura FacturaIdFacturaNavigation { get; set; } = null!;
}

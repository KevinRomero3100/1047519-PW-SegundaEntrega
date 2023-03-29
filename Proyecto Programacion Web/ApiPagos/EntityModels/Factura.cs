using System;
using System.Collections.Generic;

namespace ApiPagos.EntityModels;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateTime FechaDeEmision { get; set; }

    public DateTime FechaDeIngreso { get; set; }

    public int NumeroDeFactura { get; set; }

    public double ImporteTotal { get; set; }

    public string Nit { get; set; } = null!;

    public double Iva { get; set; }

    public int ClienteIdCliente { get; set; }

    public int EmpleadoIdEmpleado { get; set; }

    public virtual Cliente ClienteIdClienteNavigation { get; set; } = null!;

    public virtual ICollection<Detallefactura> Detallefacturas { get; } = new List<Detallefactura>();

    public virtual Empleado EmpleadoIdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<Facturaempleado> Facturaempleados { get; } = new List<Facturaempleado>();
}

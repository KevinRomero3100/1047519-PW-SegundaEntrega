using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Factura
{
    public int IdFacturaM { get; set; }

    public DateTime FechaDeEmisionM { get; set; }

    public DateTime FechaDeIngresoM { get; set; }

    public int NumeroDeFacturaM { get; set; }

    public double ImporteTotalM { get; set; }

    public double IvaM { get; set; }

    public Detallefactura DetallefacturaM { get; set; } = null!;

    public Empleado EmpleadoM { get; set; } = null!;
}

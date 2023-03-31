using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class FacturaM
    {
        public int IdFactura { get; set; }

        public DateTime FechaDeEmision { get; set; }

        public DateTime FechaDeIngreso { get; set; }

        public int NumeroDeFactura { get; set; }

        public double ImporteTotal { get; set; }

        public double Iva { get; set; }

        public DetallefacturaM Detallefactura { get; set; } = null!;

        public EmpleadoM Empleado { get; set; } = null!;
    }
}

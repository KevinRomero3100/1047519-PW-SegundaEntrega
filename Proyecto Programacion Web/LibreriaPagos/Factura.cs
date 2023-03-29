using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class Factura
    {
        public int IdFactura { get; set; }

        public DateTime FechaDeEmision { get; set; }

        public DateTime FechaDeIngreso { get; set; }

        public int NumeroDeFactura { get; set; }

        public double ImporteTotal { get; set; }

        public double Iva { get; set; }

        public Detallefactura Detallefactura { get; set; } = null!;

        public Empleado Empleado { get; set; } = null!;
    }
}

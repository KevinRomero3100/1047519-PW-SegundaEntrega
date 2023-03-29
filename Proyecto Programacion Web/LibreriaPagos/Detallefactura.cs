using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class Detallefactura
    {
        public int IdDetalle { get; set; }

        public sbyte TipodeServicio { get; set; }

        public int FacturaIdFactura { get; set; }

        public int MenusualidadIdMenusualidad { get; set; }

        public  Mensualidad Mensualidad { get; set; } = null!;
    }
}

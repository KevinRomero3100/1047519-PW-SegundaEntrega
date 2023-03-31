using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class ClienteM
    {
        public int IdCliente { get; set; }

        public int CodigoPersonal { get; set; }

        public int Dpi { get; set; }

        public string PrimerNombre { get; set; } = null!;

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; } = null!;

        public string? SegundoApellido { get; set; }

        public int Telefono { get; set; }

        public string Email { get; set; } = null!;

        public sbyte Estado { get; set; }

        public  ICollection<DireccionClienteM> DireeccionesC { get; } = new List<DireccionClienteM>();

        public ICollection<FacturaM> FacturasC { get; } = new List<FacturaM>();
    }
}

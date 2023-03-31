using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class EmpleadoM
    {
        public int IdEmpleado { get; set; }

        public int CodigoPersonal { get; set; }

        public int Dpi { get; set; }

        public string PrimerNombre { get; set; } = null!;

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; } = null!;

        public string? SegundoApellido { get; set; }

        public int Telefono { get; set; }

        public string Email { get; set; } = null!;

        public sbyte Estado { get; set; }

        public int RolIdRol { get; set; }

        public virtual ICollection<DireccionM> DireccionEmpleados { get; } = new List<DireccionM>();

        public virtual ICollection<FacturaM> Facturas { get; } = new List<FacturaM>();

        public virtual RolM RolIdRolNavigation { get; set; } = null!;

        public virtual ICollection<UsuarioM> Usuarios { get; } = new List<UsuarioM>();
    }
}

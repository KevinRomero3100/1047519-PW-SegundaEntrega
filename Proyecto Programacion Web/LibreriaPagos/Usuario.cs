using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaPagos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string NombreDeUsuario { get; set; } = null!;

        public string Pasword { get; set; } = null!;

        public int EmpleadoIdEmpleado { get; set; }

        public virtual Empleado EmpleadoIdEmpleadoNavigation { get; set; } = null!;

    }
}

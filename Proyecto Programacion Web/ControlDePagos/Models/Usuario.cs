using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Usuario
{
    public int IdUsuarioM { get; set; }

    public string NombreDeUsuarioM { get; set; } = null!;

    public string PaswordM { get; set; } = null!;

    public int EmpleadoIdEmpleadoM { get; set; }

    public virtual Empleado EmpleadoIdEmpleadoNavigationM { get; set; } = null!;

}

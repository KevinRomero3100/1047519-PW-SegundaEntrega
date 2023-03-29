using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Empleado
{
    public int IdEmpleadoM { get; set; }

    public int CodigoPersonalM { get; set; }

    public int DpiM { get; set; }

    public string PrimerNombreM { get; set; } = null!;

    public string? SegundoNombreM { get; set; }

    public string PrimerApellidoM { get; set; } = null!;

    public string? SegundoApellidoM { get; set; }

    public int TelefonoM { get; set; }

    public string EmailM { get; set; } = null!;

    public sbyte EstadoM { get; set; }

    public int RolIdRolM { get; set; }

    public virtual ICollection<Direccion> DireccionEmpleadosM { get; } = new List<Direccion>();

    public virtual ICollection<Factura> FacturasM { get; } = new List<Factura>();

    public virtual Rol RolIdRolNavigationM { get; set; } = null!;

    public virtual ICollection<Usuario> UsuariosM { get; } = new List<Usuario>();
}

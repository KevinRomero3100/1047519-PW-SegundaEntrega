using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Cliente
{
    public int IdClienteM { get; set; }

    public int CodigoPersonalM { get; set; }

    public int DpiM { get; set; }

    public string PrimerNombreM { get; set; } = null!;

    public string? SegundoNombreM { get; set; }

    public string PrimerApellidoM { get; set; } = null!;

    public string? SegundoApellidoM { get; set; }

    public int TelefonoM { get; set; }

    public string EmailM { get; set; } = null!;

    public sbyte EstadoM { get; set; }

    public  ICollection<DireccionCliente> DireeccionesCM { get; } = new List<DireccionCliente>();

    public ICollection<Factura> FacturasCM { get; } = new List<Factura>();
}

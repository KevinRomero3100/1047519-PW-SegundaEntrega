using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Direccion
{
    public int IdDireccionM { get; set; }

    public string ReferenciaM { get; set; } = null!;

    public string DescripcionM { get; set; } = null!;
}

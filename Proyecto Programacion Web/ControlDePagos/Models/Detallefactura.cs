using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDePagos.Models;

public class Detallefactura
{
    public int IdDetalleM { get; set; }

    public sbyte TipodeServicioM { get; set; }

    public int FacturaIdFacturaM { get; set; }

    public int MenusualidadIdMenusualidadM { get; set; }

    public  Mensualidad MensualidadM { get; set; } = null!;
}

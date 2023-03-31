using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Detallefactura
{
    public int IdDetalle { get; set; }

    public sbyte TipodeServicio { get; set; }

    public int FacturaIdFactura { get; set; }

    public int MenusualidadIdMenusualidad { get; set; }

    public virtual Factura FacturaIdFacturaNavigation { get; set; } = null!;

    public virtual Menusualidad MenusualidadIdMenusualidadNavigation { get; set; } = null!;
}

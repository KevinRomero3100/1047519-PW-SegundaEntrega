using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class Menusualidad
{
    public int IdMenusualidad { get; set; }

    public int Mes { get; set; }

    public int Año { get; set; }

    public virtual ICollection<Detallefactura> Detallefacturas { get; } = new List<Detallefactura>();
}

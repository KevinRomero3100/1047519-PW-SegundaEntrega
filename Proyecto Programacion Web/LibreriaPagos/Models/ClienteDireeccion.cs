using System;
using System.Collections.Generic;

namespace LibreriaPagos.Models;

public partial class ClienteDireeccion
{
    public int IdClienteDireeccion { get; set; }

    public int ColoniaIdColonia { get; set; }

    public int DireccionIdDireccion { get; set; }

    public int ClienteIdCliente { get; set; }

    public virtual Cliente ClienteIdClienteNavigation { get; set; } = null!;

    public virtual Colonium ColoniaIdColoniaNavigation { get; set; } = null!;

    public virtual Direccion DireccionIdDireccionNavigation { get; set; } = null!;
}

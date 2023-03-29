using System;
using System.Collections.Generic;

namespace ApiPagos.EntityModels;

public partial class Direccion
{
    public int IdDireccion { get; set; }

    public string Referencia { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ClienteDireeccion> ClienteDireeccions { get; } = new List<ClienteDireeccion>();

    public virtual ICollection<DireccionEmpleado> DireccionEmpleados { get; } = new List<DireccionEmpleado>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_productos.Models;

public partial class Marca
{
    [Display(Name = "ID")]
    public int IdMarca { get; set; }

    [Display(Name = "Descripcion")]
    public string Descripcion { get; set; } = null!;

    [Display(Name = "Fecha ult modificacion")]
    public DateTime FechaModificacion { get; set; }

    [Display(Name = "Baja")]
    public bool Baja { get; set; }

    [Display(Name = "Fecha baja")]
    public DateTime? FechaBaja { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}

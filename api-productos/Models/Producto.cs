using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_productos.Models;

public partial class Producto
{
    [Display(Name = "Codigo")]
    public string CodigoProducto { get; set; } = null!;

    [Display(Name = "Descripcion")]
    public string DescripcionProducto { get; set; } = null!;

    [Display(Name = "Precio Costo")]
    public decimal PrecioCosto { get; set; }

    [Display(Name = "Precio Venta")]
    public decimal PrecioVenta { get; set; }

    [Display(Name = "ID Marca")]
    public int IdMarca { get; set; }

    [Display(Name = "ID Familia")]
    public int IdFamilia { get; set; }

    [Display(Name = "Fecha ult modificacion")]
    public DateTime FechaModificacion { get; set; }

    [Display(Name = "Baja")]
    public bool Baja { get; set; }

    [Display(Name = "Fecha baja")]
    public DateTime? FechaBaja { get; set; }

    public virtual Familia IdFamiliaNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;
}

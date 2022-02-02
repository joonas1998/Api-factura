using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Api_Factura.Models
{
    public partial class Producto
    {
        public Producto()
        {
            FaacturaProductos = new HashSet<FaacturaProducto>();
        }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal ValorUnitario { get; set; }
        [JsonIgnore]
        public virtual ICollection<FaacturaProducto> FaacturaProductos { get; set; }
    }
}

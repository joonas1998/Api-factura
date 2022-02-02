using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Api_Factura.Models
{
    public partial class FaacturaProducto
    {
        public int Id { get; set; }
        public string CodigoProducto { get; set; }
        public string NumeroFactura { get; set; }
        public int CantidadVendida { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Producto CodigoProductoNavigation { get; set; }
        [JsonIgnore]
        public virtual Factura NumeroFacturaNavigation { get; set; }
    }
}

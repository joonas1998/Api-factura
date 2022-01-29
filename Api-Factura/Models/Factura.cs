﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Api_Factura.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FaacturaProductos = new HashSet<FaacturaProducto>();
        }

        public string NumeroFactura { get; set; }
        public string IdentificacionCliente { get; set; }
        public decimal TotalFactura { get; set; }

        public virtual Cliente IdentificacionClienteNavigation { get; set; }
        public virtual ICollection<FaacturaProducto> FaacturaProductos { get; set; }
    }
}

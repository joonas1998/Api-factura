using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Api_Factura.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Tipoidentificacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Factura> Facturas { get; set; }
        

    }
}

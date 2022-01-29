using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Factura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {

        [HttpPost]

        public async Task<IActionResult> PostFactura([FromBody] Models.Factura factura)
        {


            using (var db = new Models.BD_facturaContext())
            {
                if (factura == null )
                {
                    return BadRequest(ModelState);
                }
                else if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }else {

                    Models.Factura objfactura = new Models.Factura();

                    objfactura.NumeroFactura = factura.NumeroFactura;
                    objfactura.IdentificacionCliente = factura.IdentificacionCliente;
                    objfactura.TotalFactura = factura.TotalFactura;

                    await db.AddAsync(objfactura);
                    await db.SaveChangesAsync();

                    foreach (var oConcepto in factura.FaacturaProductos)
                    {
                        Models.FaacturaProducto Fconcepto = new Models.FaacturaProducto();
                        Fconcepto.CodigoProducto = oConcepto.CodigoProducto;
                        Fconcepto.NumeroFactura = objfactura.NumeroFactura;
                        Fconcepto.CantidadVendida = oConcepto.CantidadVendida;
                        Fconcepto.Total = oConcepto.Total;

                        await db.AddAsync(Fconcepto);


                    }
                    await db.SaveChangesAsync();

                    return Ok(objfactura);
                }
              

            }

        }
        [HttpGet]
        public async Task<IActionResult> GetFacturas()
        {

            using (var db = new Models.BD_facturaContext())
            {
                var listaFacturas = await db.Facturas.OrderBy(c => c.NumeroFactura).ToListAsync();
                return Ok(JsonConvert.SerializeObject(listaFacturas));

            }
        }

    }
}

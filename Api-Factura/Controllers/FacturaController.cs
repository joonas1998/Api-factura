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

            //se abre la conexion a la BD
            using (var db = new Models.BD_facturaContext())
            {   //se verifica que el objeto enviado no sea invalido o null
                if (factura == null )
                {
                    return BadRequest(ModelState);
                }
                else if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }else {
                    //se crea un objeto de la clase factura y se carga con el objeto que llega por post
                    Models.Factura objfactura = new Models.Factura();
                    
                    objfactura.NumeroFactura = factura.NumeroFactura;
                    objfactura.IdentificacionCliente = factura.IdentificacionCliente;
                    objfactura.TotalFactura = factura.TotalFactura;
                    //se regista la venta 
                    await db.AddAsync(objfactura);
                    await db.SaveChangesAsync();

                    //se recorre la lista de conceptos que llega por post y cada uno se carga en un objeto 
                    //de tipo FaacturaProducto(concepto)
                    foreach (var oConcepto in factura.FaacturaProductos)
                    {
                        Models.FaacturaProducto Fconcepto = new Models.FaacturaProducto();
                        Fconcepto.CodigoProducto = oConcepto.CodigoProducto;
                        Fconcepto.NumeroFactura = objfactura.NumeroFactura;
                        Fconcepto.CantidadVendida = oConcepto.CantidadVendida;
                        Fconcepto.Total = oConcepto.Total;

                        await db.AddAsync(Fconcepto);


                        // una vez agregado el concepto se actualiza el stock del producto
                        Models.Producto objProducto = new Models.Producto();

                        // se carga en objeto de tipo Producto con el resultado de la consulta
                        objProducto = await db.Productos.FirstOrDefaultAsync(c => c.Codigo == Fconcepto.CodigoProducto);
                        //se actualiza el stock restando al valor del stock actual la cantidad vendida en el concepto
                        objProducto.Stock = objProducto.Stock - Fconcepto.CantidadVendida;

                         db.Productos.Attach(objProducto);
                         db.Entry(objProducto).Property(x => x.Stock).IsModified = true;
                        await db.SaveChangesAsync();



                    }
                    

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
        
        //consulta el poducto para modificar su stock
        async void GetProduct(string codigo)
        {
            using (var db = new Models.BD_facturaContext())
            {
                var objproducto = await db.Productos.FirstOrDefaultAsync(c => c.Codigo == codigo);
                
            }
              
        }

    }
}

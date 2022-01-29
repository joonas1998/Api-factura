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
    public class ProductoController : ControllerBase
    {

        [HttpPost]

        public async Task<IActionResult> GuardarProducto([FromBody] Models.Producto producto)
        {
            using (var db = new Models.BD_facturaContext())
            {
                if (producto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await db.AddAsync(producto);
                await db.SaveChangesAsync();

                return Ok(JsonConvert.SerializeObject(producto));

            }


        }

        
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            using (var db = new Models.BD_facturaContext())
            {
                var listaProductos = await db.Productos.OrderBy(c => c.Descripcion).ToListAsync();
                return Ok(listaProductos);

            }

        }
        
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetProduct(string codigo)
        {

            using (var db = new Models.BD_facturaContext())
            {
                var objproducto= await db.Productos.FirstOrDefaultAsync(c => c.Codigo == codigo);
                return Ok(JsonConvert.SerializeObject(objproducto));

            }
        }
    }
}

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
    public class ClienteController : ControllerBase
    {

        [HttpPost]

        public async Task<IActionResult> PostCliente([FromBody] Models.Cliente cliente)
        {
            using (var db = new Models.BD_facturaContext())
            {

                if (cliente == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await db.AddAsync(cliente);
                await db.SaveChangesAsync();
                clase.Cliente Cliente = new clase.Cliente();

                Cliente.Identificacion = cliente.Identificacion;
                Cliente.Nombre = cliente.Nombre;
                Cliente.Apellido = cliente.Apellido;
                Cliente.Direccion = cliente.Direccion;
                Cliente.Tipoidentificacion = cliente.Tipoidentificacion;

                return Ok(JsonConvert.SerializeObject(Cliente));


            }


        }


        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {

            using (var db = new Models.BD_facturaContext())
            {
                var listaClientes = await db.Clientes.OrderBy(c => c.Nombre).ToListAsync();
                string jsonstring= System.Text.Json.JsonSerializer.Serialize(listaClientes);
                return Ok(jsonstring);
                

            }
        }

        
        [HttpGet("{Id}")]
        public async Task<IActionResult> Getcliente(string Id)
        {


            using (var db = new Models.BD_facturaContext())
            {
                if (Id == null)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var objcliente = await db.Clientes.FirstOrDefaultAsync(c => c.Identificacion == Id);
                        string jsonCliente = System.Text.Json.JsonSerializer.Serialize(objcliente);
                        return Ok(jsonCliente);
                    }
                }
            }
        }
    }
}

using ProyectoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public ProductosController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [AllowAnonymous] //TUVE QUE DARLE ALLOWANONYMOUS A TODOS LOS METODOS NO SE PORQUE PARA QUE ME DEJARAN EJECUTARLOS (REVISAR)
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<ProductosEnt>("ConsultarProductos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegistrarProducto")]
        public IActionResult RegistrarProducto(ProductosEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("RegistrarProducto",
                        new { entidad.nombreProducto, entidad.descripcion, entidad.precio, entidad.cantidadStock, entidad.marca, entidad.tipoProducto },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConsultarProductoPorId/{id}")]
        public IActionResult ConsultarProductoPorId(long id)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<ProductosEnt>("ConsultarProductoPorId",
                        new { IdProducto = id },
                        commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (datos != null)
                    {
                        return Ok(datos);
                    }
                    else
                    {
                        return NotFound($"No se encontró el producto con ID: {id}");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("ActualizarProducto")]
        public IActionResult ActualizarProducto(ProductosEnt entidad)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarProducto",
                        new { entidad.nombreProducto, entidad.descripcion, entidad.precio, entidad.cantidadStock, entidad.marca, entidad.tipoProducto, entidad.IdProducto },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "Admin")] 

        //Revisar a detalle este método
        [HttpDelete]
        [AllowAnonymous]
        [Route("EliminarProducto/{idProducto}")]
        public IActionResult EliminarProducto(long idProducto)
        {
            try
            {
                // Verificar si el ID proporcionado es válido.
                if (idProducto < 0)
                {
                    return BadRequest("ID de producto no válido.");
                }

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarProducto",
                        new { IdProducto = idProducto },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet] //PARA EL DROPDOWN
        [AllowAnonymous]
        [Route("ConsultarTipoProducto")]
        public IActionResult ConsultarTipoProducto()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("ConsultarTipoProducto",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}





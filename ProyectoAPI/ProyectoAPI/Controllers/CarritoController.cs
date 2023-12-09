using ProyectoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public CarritoController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Authorize]
        [Route("RegistrarCarrito")]
        public IActionResult RegistrarCarrito(CarritoEnt entidad)
        {
            try
            {
                long IdUsuario = _utilitarios.ObtenerUsuario(User.Claims);

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("RegistrarCarrito",
                        new { IdUsuario, entidad.IdProducto, entidad.Cantidad },
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
        [Authorize]
        [Route("ConsultarCarrito")]
        public IActionResult ConsultarCarrito()
        {
            try
            {
                long IdUsuario = _utilitarios.ObtenerUsuario(User.Claims);

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<CarritoEnt>("ConsultarCarrito",
                        new { IdUsuario },
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
        [Authorize]
        [Route("PagarCarrito")]
        public IActionResult PagarCarrito()
        {
            try
            {
                long IdUsuario = _utilitarios.ObtenerUsuario(User.Claims);

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<string>("PagarCarrito",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("EliminarProductoCarrito")]
        public IActionResult EliminarProductoCarrito(long q)
        {
            try
            {
                long IdCarrito = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarProductoCarrito",
                        new { IdCarrito },
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
        [Authorize]
        [Route("ConsultarFacturas")]
        public IActionResult ConsultarFacturas()
        {
            try
            {
                long IdUsuario = _utilitarios.ObtenerUsuario(User.Claims);

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<FacturasEnt>("ConsultarFacturas",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("ConsultarDetalleFactura")]
        public IActionResult ConsultarDetalleFactura(long q)
        {
            try
            {
                long IdFactura = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<FacturasEnt>("ConsultarDetalleFactura",
                        new { IdFactura },
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

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
                        new {  },
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






        //Revisar Este no estoy muy seguro que funcione debido , funciona desde el swagger dandole el id lo cual no es lo optimo
      

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
        
		[HttpPost]
		[Route("EliminarProducto")]   //ESTA FUNCIONAL EL PROCEDIMIENTO ALMACENADO DESDE EL SQL SERVER , EN SWAGGER NO ENCUENTRA EL IDPRODUCTO
		public IActionResult EliminarProducto(ProductosEnt entidad)
		{
			try
			{
				using (var context = new SqlConnection(_connection))
				{
					var datos = context.Execute("EliminarProducto",
						new {  entidad.IdProducto }, 
						commandType: CommandType.StoredProcedure);

					if (datos > 0)
					{
						return Ok($"Se eliminó el producto con ID: {entidad.IdProducto}");
					}
					else
					{
						return NotFound($"No se encontró el producto con ID: {entidad.IdProducto}");
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		 

		[HttpGet] //PARA EL DROPDOWN
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





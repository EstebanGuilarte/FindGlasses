using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Entities;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;
        private readonly IUsuarioModel _usuarioModel;

        public CarritoController(ICarritoModel carritoModel, IUsuarioModel usuarioModel)
        {
            _carritoModel = carritoModel;
            _usuarioModel = usuarioModel;
        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult RegistrarCarrito(long IdProducto, int cantidadComprar)
        {
            var entidad = new CarritoEnt();
            entidad.Cantidad = cantidadComprar;
            entidad.IdProducto = IdProducto;

            _carritoModel.RegistrarCarrito(entidad);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            return Json("OK");
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ConsultarCarrito()
        {


            var datos = _carritoModel.ConsultarCarrito();
            return View(datos);
        }



        [HttpGet]
        [FiltroSeguridad]
        public IActionResult Pagar()
        {
            var usuario = _usuarioModel.ConsultarUsuario(0); // Suponiendo que ConsultarUsuario devuelve un UsuarioEnt
            var carritoItems = _carritoModel.ConsultarCarrito(); // Suponiendo que ConsultarCarrito devuelve una lista de CarritoEnt

            var viewModel = new CombinacionEnt
            {
                Usuario = usuario,
                CarritoItems = carritoItems
            };

            ViewBag.Provincias = _usuarioModel.ConsultarProvincias();

            return View(viewModel); // Pasar el ViewModel a la vista
        }




		//ESTO SIRVE PARA VER LA INFO DEL USUARIO EN EL PAGAR PERO QUIERO AGREGAR LA DEL CARRITO TAMBIEN
		//[HttpGet]
		//[FiltroSeguridad]
		//public IActionResult Pagar()
		//{

		//	ViewBag.Provincias = _usuarioModel.ConsultarProvincias();
		//	var datos = _usuarioModel.ConsultarUsuario(0);


		//	var viewModel = new CombinacionEnt();



		//	return View(datos);
		//}




		//[HttpPost]
		//[FiltroSeguridad]
		//public IActionResult PagarCarrito()
		//{
		//    var respuesta = _carritoModel.PagarCarrito();
		//    var datos = _carritoModel.ConsultarCarrito();

		//    HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
		//    HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

		//    if (respuesta.Contains("verifique"))
		//    {
		//        ViewBag.MensajePantalla = respuesta;
		//        return View("ConsultarCarrito", datos);
		//    }
		//    else
		//    {
		//        return RedirectToAction("Factura", "Carrito");
		//    }
		//}



		[HttpPost]
		[FiltroSeguridad]
		public IActionResult PagarCarrito()
		{
			var respuesta = _carritoModel.PagarCarrito();
			var datos = _carritoModel.ConsultarCarrito();

			HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
			HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

			if (respuesta.Contains("verifique"))
			{
				ViewBag.MensajePantalla = respuesta;
				return View("ConsultarCarrito", datos);
			}
			else
			{
				// Obtener IdUsuario almacenado en la sesión
				if (HttpContext.Session.TryGetValue("IdUsuario", out byte[] idUsuarioBytes) && idUsuarioBytes != null)
				{
					string idUsuario = System.Text.Encoding.UTF8.GetString(idUsuarioBytes);

					if (!string.IsNullOrEmpty(idUsuario) && long.TryParse(idUsuario, out long idUsuarioParsed))
					{
						return RedirectToAction("Factura", "Carrito", new { q = idUsuarioParsed });
					}
					else
					{
						return View("Error");
					}
				}
				else
				{
					return View("Error");
				}
			}
		}






		[HttpGet]
		[FiltroSeguridad]
		public IActionResult Factura(long q)
		{


			var datos = _carritoModel.ConsultarUltimaFacturaYDetalles(q);
			return View(datos);
		}






		//SIRVE PERFECTO

		//[HttpPost]
		//[FiltroSeguridad]
		//public IActionResult PagarCarrito()
		//{
		//	var respuesta = _carritoModel.PagarCarrito();
		//	var datos = _carritoModel.ConsultarCarrito();

		//	HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
		//	HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

		//	if (respuesta.Contains("verifique"))
		//	{
		//		ViewBag.MensajePantalla = respuesta;
		//		return View("ConsultarCarrito", datos);
		//	}
		//	else
		//	{
		//		return RedirectToAction("Index", "Login");
		//	}
		//}








		[HttpGet]
        [FiltroSeguridad]
        public IActionResult EliminarProductoCarrito(long q)
        {
            _carritoModel.EliminarProductoCarrito(q);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            return RedirectToAction("Pagar", "Carrito");
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ConsultarFacturas()
        {
            var datos = _carritoModel.ConsultarFacturas();
            return View(datos);
        }

        //[HttpGet]
        //[FiltroSeguridad]
        //public IActionResult ConsultarDetalleFactura(long q)
        //{
        //    var datos = _carritoModel.ConsultarUltimaFacturaYDetalles(q);
        //    return View(datos);
        //}
    }
}




using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Entities;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;

        public CarritoController(ICarritoModel carritoModel)
        {
            _carritoModel = carritoModel;
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
                return RedirectToAction("Index", "Login");
            }            
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult EliminarProductoCarrito(long q)
        {
            _carritoModel.EliminarProductoCarrito(q);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            return RedirectToAction("ConsultarCarrito", "Carrito");
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ConsultarFacturas()
        {
            var datos = _carritoModel.ConsultarFacturas();
            return View(datos);
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ConsultarDetalleFactura(long q)
        {
            var datos = _carritoModel.ConsultarDetalleFactura(q);
            return View(datos);
        }
    }
}

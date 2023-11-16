using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Entities;
using ProyectoWeb.Models;
using System.Diagnostics;

namespace ProyectoWeb.Controllers
{

    public class ProductosController : Controller
    {
        private readonly IProductosModel _productosModel;

        public ProductosController(IProductosModel productoModel)
        {
            _productosModel = productoModel;
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ProductosLista()
        {
            var productos = _productosModel.ConsultarProductos();
            return View(productos);
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult RegistrarProducto(ProductosEnt entidad)
        {
            if (_productosModel.RegistrarProducto(entidad))
            {
                return RedirectToAction("ProductosLista");
            }
            else
            {
                return View(entidad);
            }
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult DetalleProducto(long id)
        {
            var producto = _productosModel.ConsultarProductoPorId(id);

            if (producto != null)
            {
                return View(producto);
            }
            else
            {
                return RedirectToAction("ProductosLista");
            }
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ActualizarProducto(long id)
        {
            // Obtener el producto por id 
            var producto = _productosModel.ConsultarProductoPorId(id);

            if (producto != null)
            {
                return View(producto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult ActualizarProducto(ProductosEnt entidad)
        {
            if (_productosModel.ActualizarProducto(entidad))
            {
                return RedirectToAction("ProductosLista");
            }
            else
            {
                return View(entidad);
            }
        }

        //Error Pendiente
        [HttpPost]
        [FiltroSeguridad]
        public IActionResult EliminarProducto(ProductosEnt entidad)
        {
            var exito = _productosModel.EliminarProducto(entidad.IdProducto);

            if (exito)
            {
                TempData["Mensaje"] = $"Se eliminó el producto con ID: {entidad.IdProducto}";
            }
            else
            {
                TempData["Mensaje"] = $"No se encontró el producto con ID: {entidad.IdProducto}";
            }

            return RedirectToAction("ProductosLista");
        }



    }
}
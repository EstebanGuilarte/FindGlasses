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
        public IActionResult ProductosLista()
        {
            return View();
        }



    }
}
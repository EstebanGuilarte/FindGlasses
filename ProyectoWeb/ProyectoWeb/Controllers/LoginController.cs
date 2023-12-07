﻿using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Entities;
using ProyectoWeb.Models;
using System.Diagnostics;

namespace WEBJN.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class LoginController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        /*
        private readonly IProductosModel _productoModel;
        private readonly ICarritoModel _carritoModel;
        */

        public LoginController(IUsuarioModel usuarioModel /*,IProductosModel productoModel, ICarritoModel carritoModel*/)
        {
            _usuarioModel = usuarioModel;
            /*
            _productoModel = productoModel;
            _carritoModel = carritoModel;
            */
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.IniciarSesion(entidad);

            if (resp != null)
            {
                HttpContext.Session.SetString("NombreUsuario", resp.nombre);
                HttpContext.Session.SetString("TokenUsuario", resp.Token);
                HttpContext.Session.SetString("RolUsuario", resp.ConRol.ToString());

                /*
                var datos = _carritoModel.ConsultarCarrito();
                HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
                HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());
                */

                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo validar su cuenta";
                return View();
            }
        }

        [HttpGet]
        public IActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RegistrarCuenta(entidad);

            if (resp == 1)
                return RedirectToAction("IniciarSesion", "Login");
            else
            {
                ViewBag.MensajePantalla = "No se pudo registrar su cuenta";
                return View();
            }
        }

        [HttpGet]
        public IActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RecuperarCuenta(entidad);

            if (resp == 1)
                return RedirectToAction("IniciarSesion", "Login");
            else
            {
                ViewBag.MensajePantalla = "No se pudo validar su cuenta";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CambiarClaveCuenta(string q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuarioSeguro = q;
            return View(entidad);
        }

        [HttpPost]
        public IActionResult CambiarClaveCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.CambiarClaveCuenta(entidad);

            if (resp == 1)
                return RedirectToAction("IniciarSesion", "Login");
            else
            {
                ViewBag.MensajePantalla = "No se pudo actualizar su contraseña";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Entities;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;

        public UsuarioController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }


        [HttpGet]
        [FiltroSeguridad]
        public IActionResult PerfilUsuario()
        {
            var datos = _usuarioModel.ConsultarUsuario();
            ViewBag.Provincias = _usuarioModel.ConsultarProvincias();
            return View(datos);
        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult PerfilUsuario(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.ActualizarCuenta(entidad);

            if (resp == 1)
            {
                HttpContext.Session.SetString("NombreUsuario", entidad.nombre);
                ViewBag.MensajePantalla = "Su cuenta se actualizó correctamente";
            }
            else
                ViewBag.MensajePantalla = "No se pudo actualizar su cuenta";

            ViewBag.Provincias = _usuarioModel.ConsultarProvincias();
            return View();
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ConsultarUsuarios()
        {
            var datos = _usuarioModel.ConsultarUsuarios();
            return View(datos);
        }
    }
}

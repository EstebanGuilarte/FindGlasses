using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Entities;

namespace ProyectoWeb.Models
{
    public interface IUsuarioModel
    {

        public UsuarioEnt? IniciarSesion(UsuarioEnt entidad);

        public int RegistrarCuenta(UsuarioEnt entidad);

        public int RecuperarCuenta(UsuarioEnt entidad);

        public int CambiarClaveCuenta(UsuarioEnt entidad);

        public UsuarioEnt? ConsultarUsuario();

        public int ActualizarCuenta(UsuarioEnt entidad);

        public List<UsuarioEnt>? ConsultarUsuarios();

        public List<SelectListItem>? ConsultarProvincias();

    }
}

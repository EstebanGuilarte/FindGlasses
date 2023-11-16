using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Entities;

namespace ProyectoWeb.Models
{
    public interface IProductosModel
    {
        List<ProductosEnt> ConsultarProductos();
        bool RegistrarProducto(ProductosEnt entidad);
        ProductosEnt ConsultarProductoPorId(long id);
        bool ActualizarProducto(ProductosEnt entidad);
        bool EliminarProducto(long idProducto);
    }
}

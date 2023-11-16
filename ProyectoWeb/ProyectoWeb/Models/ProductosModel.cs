using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ProyectoWeb.Entities;
using System.Threading.Tasks;
using System.Net;

namespace ProyectoWeb.Models
{
    public class ProductosModel : IProductosModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public ProductosModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }


        public List<ProductosEnt> ConsultarProductos()
        {
            string url = _urlApi + "api/Productos/ConsultarProductos";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductosEnt>>().Result;
            else
                return new List<ProductosEnt>();
        }

        public bool RegistrarProducto(ProductosEnt entidad)
        {
            try
            {
                string url = _urlApi + "api/Productos/RegistrarProducto";
                string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = _httpClient.PostAsJsonAsync(url, entidad).Result;

                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductosEnt ConsultarProductoPorId(long id)
        {
            string url = $"{_urlApi}api/Productos/ConsultarProductoPorId/{id}";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ProductosEnt>().Result;
            else
                return null;
        }

        public bool ActualizarProducto(ProductosEnt entidad)
        {
            try
            {
                string url = _urlApi + "api/Productos/ActualizarProducto";
                string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = _httpClient.PutAsJsonAsync(url, entidad).Result;

                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarProducto(long idProducto)
        {
            try
            {
                string url = $"{_urlApi}api/Productos/EliminarProducto/{idProducto}";
                string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = _httpClient.DeleteAsync(url).Result;

                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }




    }
}

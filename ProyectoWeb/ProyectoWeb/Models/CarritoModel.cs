using System.Net.Http.Headers;
using ProyectoWeb.Entities;

namespace ProyectoWeb.Models
{
    public class CarritoModel : ICarritoModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public CarritoModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public long RegistrarCarrito(CarritoEnt entidad)
        {
            string url = _urlApi + "api/Carrito/RegistrarCarrito";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<long>().Result;
            else
                return 0;
        }

        public List<CarritoEnt>? ConsultarCarrito()
        {
            string url = _urlApi + "api/Carrito/ConsultarCarrito";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
            else
                return null;
        }

        public string PagarCarrito()
        {
            var entidad = new CarritoEnt();
            string url = _urlApi + "api/Carrito/PagarCarrito";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                //return resp.Content.ReadFromJsonAsync<string>().Result;
                return resp.Content.ReadAsStringAsync().Result;
            else
                return string.Empty;
        }

        public int EliminarProductoCarrito(long q)
        {
            string url = _urlApi + "api/Carrito/EliminarProductoCarrito?q=" + q;
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public List<FacturasEnt>? ConsultarFacturas()
        {
            string url = _urlApi + "api/Carrito/ConsultarFacturas";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<FacturasEnt>>().Result;
            else
                return null;
        }

        public List<FacturasEnt>? ConsultarDetalleFactura(long q)
        {
            string url = _urlApi + "api/Carrito/ConsultarDetalleFactura?q=" + q;
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<FacturasEnt>>().Result;
            else
                return null;
        }
               

    }
}

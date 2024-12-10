using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filtros;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers
{
    public class ProveedorController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [Logueado]
        [SoloOperador]
        public IActionResult Index(string mensaje, string error)
        {
            ViewBag.Proveedores = _sistema.Proveedores;
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        [Logueado]
        [SoloOperador]
        public IActionResult ActualizarDescuento(string prov, string desc)
        {
            try
            {
                if (desc == null)
                {
                    throw new Exception("Debe ingresar un valor en el campo");
                }
                decimal i = 0;
                if (!decimal.TryParse(desc, out i))
                {
                    throw new Exception("Solo puede ingresar numeros");
                }
                _sistema.ModificarDescuentoProveedor(prov, i);
                return RedirectToAction("Index", new { mensaje = $"Se a modificado el descuento de {prov} a {i}%" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { error = e.Message });
            }
        }
    }
}

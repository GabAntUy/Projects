using Microsoft.AspNetCore.Mvc;
using Dominio;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class ActividadController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index(string error)
        {
            /// muestra para usuario sin identificar y Huesped solo actividades disponibles (con cupo > 0)
            ViewBag.Actividades = _sistema.ListadoActividadesDisponiblesHoy();
            /// muestra para Operador todas las actividades (con cupo = 0 también)
            if (HttpContext.Session.GetString("rol") == "Operador")
            {
                ViewBag.Actividades = _sistema.ListadoActividadesHoy();
            }
            ViewBag.ErrorListaVacia = ViewBag.Actividades.Count == 0 ? "No hay actividades para hoy" : null;
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        public IActionResult ActividadesXFecha(DateTime fecha)
        {
            try
            {
                if (fecha == DateTime.MinValue)
                {
                    throw new Exception("Debe ingresar una fecha");
                }
                else
                {
                    /// muestra para usuario sin identificar y Huesped solo actividades disponibles (con cupo > 0)
                    ViewBag.Actividades = _sistema.ListadoActividadesDisponiblesXFecha(fecha);
                    /// muestra para Operador todas las actividades (con cupo = 0 también)
                    if (HttpContext.Session.GetString("rol") == "Operador")
                    {
                        ViewBag.Actividades = _sistema.ListadoActividadesXFecha(fecha);
                    }
                    ViewBag.ErrorListaVacia = ViewBag.Actividades.Count == 0 ? "No hay actividades para la fecha seleccionada" : null;
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("index", new { error = ViewBag.Error });
        }
    }
}

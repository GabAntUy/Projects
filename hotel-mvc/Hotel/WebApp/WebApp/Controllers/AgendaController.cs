using Microsoft.AspNetCore.Mvc;
using Dominio;
using WebApp.Filtros;


namespace WebApp.Controllers
{
    public class AgendaController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [Logueado]
        [SoloHuesped]
        public IActionResult Index(string mensaje)
        {
            string email = HttpContext.Session.GetString("email");
            ViewBag.Agendas = _sistema.ListadoAgendasActualesYFuturasDeHuespedXEmail(email);
            ViewBag.ErrorListaVacia = ViewBag.Agendas.Count == 0 ? "Aún no tiene actividades agendadas" : null;
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [Logueado]
        [SoloOperador]
        public IActionResult AgendasXFecha(string error)
        {
            ViewBag.Error = error;
            ViewBag.Agendas = _sistema.ListadoAgendasHoy();
            ViewBag.ErrorListaVacia = ViewBag.Agendas.Count == 0 ? "No hay actividades agendadas para hoy" : null;
            return View("index");
        }

        [HttpPost]
        [Logueado]
        [SoloOperador]
        public IActionResult AgendasXFecha(DateTime fecha)
        {
            try
            {
                if (fecha == DateTime.MinValue)
                {
                    throw new Exception("Debe ingresar una fecha");
                }
                else
                {
                    ViewBag.Agendas = _sistema.ListadoAgendasXFecha(fecha);
                    ViewBag.ErrorListaVacia = ViewBag.Agendas.Count == 0 ? "No hay actividades agendadas para la fecha seleccionada" : null;
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("AgendasXFecha", new { error = ViewBag.Error });
        }

        [Logueado]
        [SoloOperador]
        public IActionResult AgendasXHuesped()
        {
            ViewBag.ListaTipoDocumentos = _sistema.TipoDocumentos;
            return View();
        }

        [HttpPost]
        [Logueado]
        [SoloOperador]
        public IActionResult AgendasXHuesped(int idTipoDocumento, string numeroDocumento)
        {
            try
            {
                if (idTipoDocumento == 0)
                {
                    throw new Exception("Debe seleccionar un tipo de documento");
                }
                if (numeroDocumento == null)
                {
                    throw new Exception("Debe ingresar un número de documento");
                }
                Huesped unH = _sistema.ObtenerHuespedXDocumento(idTipoDocumento, numeroDocumento);
                if (unH == null)
                {
                    throw new Exception("No existe un huésped registrado con los datos ingresados");
                }
                ViewBag.Agendas = _sistema.ListadoAgendasXHuesped(unH);
                ViewBag.ErrorListaVacia = ViewBag.Agendas.Count == 0 ? "No hay actividades agendadas para el huésped seleccionado" : null;
                ViewBag.ListaTipoDocumentos = _sistema.TipoDocumentos;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            ViewBag.ListaTipoDocumentos = _sistema.TipoDocumentos;
            ViewBag.NumeroDocumento = numeroDocumento;
            ViewBag.IdTipoDocumento = idTipoDocumento;
            return View();
        }

        [HttpPost]
        [Logueado]
        [SoloHuesped]
        public IActionResult Create(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Usuario", new { error = $"Deberá loguearse para poder agendarse a una actividad" });
            }
            try
            {
                Actividad actividad = _sistema.ObtenerActividadPorId(id);
                if (actividad == null)
                {
                    throw new Exception($"La actividad con id '{id}' no existe");
                }
                Agenda agenda = new Agenda(_sistema.ObtenerHuespedXEmail(HttpContext.Session.GetString("email")), actividad);
                _sistema.AgregarAgenda(agenda);
                return RedirectToAction("Index", new { mensaje = $"Se creó la agenda para la actividad {actividad.NombreActividad}" });
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Index", "Actividad", new { error = ViewBag.Error });
        }

        [HttpPost]
        [Logueado]
        [SoloOperador]
        public IActionResult ConfirmarAgenda(int agenda)
        {
            _sistema.ObtenerAgendaID(agenda).SetEstado(EstadoAgenda.CONFIRMADA);
            ViewBag.Mensaje = "La agenda ha sido confirmada";
            return View(_sistema.ObtenerAgendaID(agenda));
        }
    }
}

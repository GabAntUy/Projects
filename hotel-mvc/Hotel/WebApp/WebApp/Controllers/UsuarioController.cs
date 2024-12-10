using Microsoft.AspNetCore.Mvc;
using Dominio;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [NoLogueado]
        public IActionResult Login(string error)
        {
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        [NoLogueado]
        public IActionResult Login(string email, string contrasena)
        {
            Usuario usuario = _sistema.ObtenerUsuarioXEmailYContrasenia(email, contrasena);
            if (usuario == null)
            {
                ViewBag.Error = "El email y/o la contraseña no son válidos";
                return View("login");
            }
            string rol = usuario.ObtenerRol();
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("rol", rol);
            if (rol == "Huesped")
            {
                return Redirect("/Actividad/index");
            }
            else if (rol == "Operador")
            {
                return Redirect("/Actividad/index");
            }
            return RedirectToAction("login");
        }

        [Logueado]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/actividad/index");
        }

        [Logueado]
        [SoloHuesped]
        public IActionResult VerDatosHuesped()
        {
            Huesped huesped = _sistema.ObtenerHuespedXEmail(HttpContext.Session.GetString("email"));
            return View(huesped);
        }

        [Logueado]
        [SoloOperador]
        public IActionResult VerDatosOperador()
        {
            Operador operador = _sistema.ObtenerOperadorXEmail(HttpContext.Session.GetString("email"));
            return View(operador);
        }

        [NoLogueado]
        public IActionResult Create()
        {
            ViewBag.ListaTipoDocumentos = _sistema.TipoDocumentos;
            return View(new Huesped());
        }

        [HttpPost]
        [NoLogueado]
        public IActionResult Create(Huesped huesped, int idTipoDocumento, string numeroDocumento)
        {
            try
            {
                TipoDocumento tipoDocumento = _sistema.ObtenerTipoDocumentoPorId(idTipoDocumento);
                if (tipoDocumento == null)
                {
                    throw new Exception("No se encontró el tipo de documento");
                }
                huesped.Documento.TipoDocumento = tipoDocumento;
                huesped.Documento.Numero = numeroDocumento;
                _sistema.AgregarHuesped(huesped);
                ViewBag.Mensaje = "Registro exitoso";
                return View("login");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            ViewBag.ListaTipoDocumentos = _sistema.TipoDocumentos;
            ViewBag.NumeroDocumento = numeroDocumento;
            ViewBag.IdTipoDocumento = idTipoDocumento;
            return View(huesped);
        }

        public IActionResult Redireccionador(string error)
        {
            switch (error)
            {
                case "NoLogueado":
                    ViewBag.Error = "Debes loguearte para poder acceder a la URL ingresada";
                    return View("login");
                case "NoEsHuesped":
                case "NoEsOperador":
                    ViewBag.Error = "Tu cuenta no tiene acceso a la URL ingresada";
                    return View("filtroURL");
                case "Logueado":
                    string email = HttpContext.Session.GetString("email");
                    ViewBag.Error = $"Has ingresado como {email}," +
                                    " es necesario cerrar la sesión antes de acceder como un usuario diferente o registrar un nuevo huésped";
                    return View("filtroURL");
                default:
                    return Redirect("/actividad/index");
            }
        }
    }
}

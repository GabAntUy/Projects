using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filtros;
using WebApp.Models.Usuarios;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private ICreate<Usuario> _create;
        private ILogin<Usuario> _login;
        private ICreate<Log> _log;

        public UsuarioController(ICreate<Usuario> create, ILogin<Usuario> login, ICreate<Log> log)
        {
            _create = create;
            _login = login;
            _log = log;

        }

        [Logueado("administrador", "persona")]
        public IActionResult Index()
        {
            return View();
        }

        [Logueado("administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [Logueado("administrador")]

        [HttpPost]
        public IActionResult Create(CreateUsuarioViewModel usuario)
        {
            if (usuario == null)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "No se puede crear la persona";
                return View(usuario);
            }

            if (usuario.Contrasenia1 != usuario.Contrasenia2)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View(usuario);
            }

            try
            {
                // Encriptar la contraseña antes de guardarla en la base de datos
                //string contraseniaEncriptada = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia1);
                //usuario.Contrasenia1 = contraseniaEncriptada;
                var persona = new Persona { Alias = usuario.Alias, Contrasenia = usuario.Contrasenia1 };
                _create.Create(persona);

                Logger(persona.Id);

                ViewBag.TipoMensaje = "OK";
                ViewBag.Mensaje = "Usuario creado correctamente.";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string alias, string contrasenia)
        {
            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(contrasenia))
            {
                ViewBag.TipoMensaje = "ERROR";
                @ViewBag.Mensaje = "Campos Vacios";
                return View();
            }

            try
            {
                Usuario usuario = _login.Login(alias, contrasenia);
                if (usuario != null)
                {

                    HttpContext.Session.SetString("alias", usuario.Alias);

                    string rol = "persona";

                    if (usuario is Administrador)
                    {
                        rol = "administrador";
                    }

                    HttpContext.Session.SetString("rol", rol);

                    if (rol == "persona" || rol == "administrador")
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.TipoMensaje = "ERROR";
                    @ViewBag.Mensaje = "Usuario y/o password incorrectos";
                    return View();
                }
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }

            return View();
        }


        [Logueado("administrador", "persona")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private void Logger(int id)
        {

            var log = new Log
            {
                UserName = HttpContext.Session.GetString("alias"),
                IdEntidad = id,
                TipoEntidad = "Ecosistema",
            };

            _log.Create(log);
        }

    }
}

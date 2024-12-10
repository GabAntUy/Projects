using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Amenazas;
using ApplicationLogic.UseCases.Ecosistemas;
using Azure.Identity;
using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Filtros;
using WebApp.Models.Ecosistemas;

namespace WebApp.Controllers
{
    public class EcoSistemaController : Controller
    {
        private ICreate<EcosistemaApi> _create;
        private IDelete<Ecosistema> _delete;
        private IGet<Ecosistema> _get;
        private IGetAll<Ecosistema> _getAll;
        private IUpdate<Ecosistema> _update;
        private IGetAll<Pais> _getPaises;
        private IGetAll<Amenaza> _getAmenazas;
        private IGetAll<EstadoDeConservacion> _getEstados;
        private IWebHostEnvironment _webHostEnvironment;
        private ICreate<Log> _log;
        public EcoSistemaController(
            IWebHostEnvironment webHostEnvironment, 
            ICreate<EcosistemaApi> create, 
            IDelete<Ecosistema> delete,
            IGet<Ecosistema> get, 
            IGetAll<Ecosistema> getAll, 
            IUpdate<Ecosistema> update,
            IGetAll<Pais> getPaises, 
            IGetAll<Amenaza> getAmenazas, 
            IGetAll<EstadoDeConservacion> getEstados, 
            ICreate<Log> log)
        {
            _log = log;
            _create = create;
            _delete = delete;
            _get = get;
            _getAll = getAll;
            _getPaises = getPaises;
            _update = update;
            _getAmenazas = getAmenazas;
            _getEstados = getEstados;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var models = MapeoListaEcosistema(_getAll.GetAll());
                return View(models);
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(new List<IndexEcosistemaViewModel>());
            }


        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var eco = _get.GetById(id);

                return View(MapeoUnEcosistema(eco));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                var ecosistemas = _getAll.GetAll();
                return View("index", MapeoListaEcosistema(ecosistemas));
            }

        }

        [Logueado("administrador", "persona")]
        [HttpGet]
        public IActionResult Create()
        {

            try
            {
                var eco = new CreateEcosistemaViewModel();
                return View(CargarComboboxes(eco));

            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }
        }

        [Logueado("administrador", "persona")]
        [HttpPost]
        public IActionResult Create(CreateEcosistemaViewModel ecoVM)
        {    
            try
            {
                if (ModelState.IsValid)
                {
                    var ecoApi = new EcosistemaApi { 
                        AmenazasId = ecoVM.AmenazasId, 
                        EstadoDeConservacionId = ecoVM.EstadoDeConservacionId,
                        Nombre = ecoVM.Nombre, 
                        Descripcion = ecoVM.Descripcion,
                        Area = ecoVM.Area,
                        Latitud =ecoVM.Latitud,
                        Longitud=ecoVM.Longitud,PaisesId = ecoVM.PaisesId 
                    };

                    _create.Create(ecoApi);

                    ViewBag.TipoMensaje = "OK";
                    ViewBag.Mensaje = "Ecosistema creado correctamente.";
                    var listaEcosistemas = _getAll.GetAll();
                    return View("Index", MapeoListaEcosistema(listaEcosistemas));
                }

                return View(CargarComboboxes(ecoVM));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(CargarComboboxes(ecoVM));
            }
        }

        [Logueado("administrador", "persona")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                Ecosistema unEcosistema = _get.GetById((int)id);

                if (unEcosistema == null)
                {
                    return RedirectToAction("Index");
                }
                return View(MapeoUnEcosistema(unEcosistema));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                var ecosistemas = _getAll.GetAll();
                return View("Index", MapeoListaEcosistema(ecosistemas));
            }

        }

        [Logueado("administrador", "persona")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _delete.Delete(id);

                Logger(id);

                ViewBag.TipoMensaje = "OK";
                ViewBag.Mensaje = "Ecosistema eliminado correctamente.";
                var ecositemas = _getAll.GetAll();
                return View("Index",MapeoListaEcosistema(ecositemas));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                var unEco = _get.GetById(id);
                return View(MapeoUnEcosistema(unEco));
            }
        }

        private CreateEcosistemaViewModel CargarComboboxes(CreateEcosistemaViewModel eco) 
        {
            eco.Paises = _getPaises.GetAll();
            eco.Amenazas = _getAmenazas.GetAll();
            eco.EstadoDeConservacion = _getEstados.GetAll();

            return eco;
        }

        private IEnumerable<IndexEcosistemaViewModel> MapeoListaEcosistema(IEnumerable<Ecosistema> ecosistemas)
        {
            return ecosistemas.Select(e => new IndexEcosistemaViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Area = e.Area,
                Descripcion = e.Descripcion,
                Paises = e.Paises,
                EstadoDeConservacion = e.EstadoDeConservacion,
                Amenazas = e.Amenazas,
                Ubicacion = e.Ubicacion,
                Imagenes = e.Imagenes,

                EspeciesQueLoHabitan = e.EspeciesQueLoHabitan,

            }).ToList();
        }
        private IndexEcosistemaViewModel MapeoUnEcosistema(Ecosistema e)
        {
            return new IndexEcosistemaViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Area = e.Area,
                Descripcion = e.Descripcion,
                Paises = e.Paises,
                EstadoDeConservacion = e.EstadoDeConservacion,
                Amenazas = e.Amenazas,
                Ubicacion = e.Ubicacion,
                Imagenes = e.Imagenes,
                EspeciesQueLoHabitan = e.EspeciesQueLoHabitan
            };
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

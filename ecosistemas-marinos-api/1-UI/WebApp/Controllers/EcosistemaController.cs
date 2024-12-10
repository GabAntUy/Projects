using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Amenazas;
using ApplicationLogic.UseCases.Ecosistemas;
using Azure.Identity;
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
        private ICreate<Ecosistema> _create;
        private IDelete<Ecosistema> _delete;
        private IGet<Ecosistema> _get;
        private IGetAll<Ecosistema> _getAll;
        private IUpdate<Ecosistema> _update;
        private IGetAll<Pais> _getPaises;
        private IGetAll<Amenaza> _getAmenazas;
        private IGetAll<EstadoDeConservacion> _getEstados;
        private IWebHostEnvironment _webHostEnvironment;
        private IGet<EstadoDeConservacion> _getEstadoPorId;
        private IGetSelected<Pais> _getPaisSelected;
        private IGetSelected<Amenaza> _getAmenazaSelected;
        private ICreate<Log> _log;
        public EcoSistemaController(IWebHostEnvironment webHostEnvironment, ICreate<Ecosistema> create, IDelete<Ecosistema> delete,
            IGet<Ecosistema> get, IGetAll<Ecosistema> getAll, IUpdate<Ecosistema> update,
            IGetAll<Pais> getPaises, IGetAll<Amenaza> getAmenazas, IGetAll<EstadoDeConservacion> getEstados, 
            IGet<EstadoDeConservacion> getEstadoPorId, IGetSelected<Pais> getPaisSelected, 
            IGetSelected<Amenaza> getAmenazaSelected, ICreate<Log> log)
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
            _getEstadoPorId = getEstadoPorId;
            _webHostEnvironment = webHostEnvironment;
            _getPaisSelected = getPaisSelected;
            _getAmenazaSelected = getAmenazaSelected;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = MapeoListaEcosistema(_getAll.GetAll());
            return View(models); 

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
                    var paises = _getPaisSelected.GetSelected(ecoVM.PaisesId).ToList();
                    var estado = _getEstadoPorId.GetById(ecoVM.EstadoDeConservacionId);
                    var amenazas = _getAmenazaSelected.GetSelected(ecoVM.AmenazasId).ToList();

                    var eco = new Ecosistema
                    {
                        Paises = paises,
                        EstadoDeConservacion = estado,
                        Amenazas = amenazas,
                        //Nombre = ecoVM.Nombre,
                        Area = ecoVM.Area,
                        Descripcion = ecoVM.Descripcion,
                        //Ubicacion = new UbicacionEcosistema { Latitud = ecoVM.Latitud, Longitud = ecoVM.Longitud },
                    };

                    _create.Create(eco);

                    List<ImagenEcosistema> fotos = new List<ImagenEcosistema>();

                    int count = 1;

                    foreach (var item in ecoVM.ImagenesEcosistema)
                    {
                        ImagenEcosistema img = new ImagenEcosistema
                        {
                            Nombre = Path.Combine($"/ImagenesEcosistema/{eco.Id}_{count.ToString("000")}{Path.GetExtension(item.FileName)}")
                        };

                        fotos.Add(img);

                        string rutaAbsoluta = Path.Combine($"{_webHostEnvironment.WebRootPath}\\ImagenesEcosistema\\{eco.Id}_{count.ToString("000")}{Path.GetExtension(item.FileName)}");
                        FileStream foto = new FileStream(rutaAbsoluta, FileMode.Create);
                        item.CopyTo(foto);
                        count++;
                    }

                    eco.Imagenes = fotos;
                    _update.Update(eco.Id, eco);

                    Logger(eco.Id);

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
                //Nombre = e.Nombre,
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
                //Nombre = e.Nombre,
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

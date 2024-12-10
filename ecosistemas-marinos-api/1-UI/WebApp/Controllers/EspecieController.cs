using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Ecosistemas;
using ApplicationLogic.UseCases.Especies;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApp.Filtros;
using WebApp.Models.Ecosistemas;
using WebApp.Models.Especies;

namespace WebApp.Controllers
{
    public class EspecieController : Controller
    {
        private ICreate<Especie> _create;
        //private IDelete<Especie> _delete;
        private IGet<Especie> _get;
        private IGetAll<Especie> _getAll;
        private IUpdate<Especie> _update;
        private IGetAll<EstadoDeConservacion> _getAllEstados;
        private IGetAll<Amenaza> _getAllAmenzas;
        private IGetAll<Ecosistema> _getAllEcosistemas;
        private IGet<EstadoDeConservacion> _getEstadoDeConservacion;
        private IGetSelected<Amenaza> _getAmenazaSelected;
        private IGet<Ecosistema> _getEcosistema;
        private IWebHostEnvironment _webHostEnvironment;
        private IAddEcosistema<Especie> _addEcosistema;
        private IGetSelected<Ecosistema> _getSelectedEcosistema;
        private IGetEspeciesPeligro<Especie> _especiesPeligro;
        private ICreate<Log> _log;

        //Consultas LINQ
        private IGetAllByString<Especie> _getAllByString;
        private IGetEspeciePorPeso<Especie> _listarPorpeso;
        private IListarEcosistema<Ecosistema> _listarEcosistemas;
        private IGetAllByEcosistema<Especie> _getAllByEcosistema;

        public EspecieController(
            ICreate<Especie> create, IGet<Especie> get, IGetAll<Especie> getAll, 
            IUpdate<Especie> update, IGetAll<EstadoDeConservacion> getAllEstado,
            IGetAll<Amenaza> getAllAmenazas, IGetAll<Ecosistema> getAllEcosistemas, IGet<EstadoDeConservacion> getEstadoDeConservacion,
            IGetSelected<Amenaza> getAmenazaSelected, IGet<Ecosistema> getEcosistema, IWebHostEnvironment webHostEnvironment,
            IGetAllByString<Especie> getAllByString, IGetEspeciePorPeso<Especie> listar, ICreate<Log> log,
            IAddEcosistema<Especie> addEcosistema, IGetSelected<Ecosistema> getSelectedEcosistema, 
            IListarEcosistema<Ecosistema> listarEcosistemas, IGetAllByEcosistema<Especie> getAllByEcosistema, IGetEspeciesPeligro<Especie> especiesPeligro
            )
        {
            _log = log;
            _create = create;
            //_delete = delete;
            _get = get;
            _getAll = getAll;
            _update = update;
            _getAllEstados = getAllEstado;
            _getAllAmenzas = getAllAmenazas;
            _getAllEcosistemas = getAllEcosistemas;
            _getEstadoDeConservacion = getEstadoDeConservacion;
            _getAmenazaSelected = getAmenazaSelected;
            _getEcosistema = getEcosistema;
            _webHostEnvironment = webHostEnvironment;
            _getAllByString = getAllByString;
            _listarPorpeso = listar;
            _addEcosistema = addEcosistema;
            _getSelectedEcosistema = getSelectedEcosistema;
            _listarEcosistemas = listarEcosistemas;
            _getAllByEcosistema = getAllByEcosistema;
            _especiesPeligro = especiesPeligro;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var especies = _getAll.GetAll();
                return View(MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View();
            }

        }

        [Logueado("administrador", "persona")]
        public IActionResult Create()
        {
            try
            {
                var especieVM = new CreateEspecieViewModel();
                especieVM.EstadosDeConservacion = _getAllEstados.GetAll();
                especieVM.Amenazas = _getAllAmenzas.GetAll();
                especieVM.PuedeHabitar = _getAllEcosistemas.GetAll();

                
                return View(especieVM);
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
        public IActionResult Create(CreateEspecieViewModel especieVM)
        {
            try
            {
                EstadoDeConservacion estadoDeConservacion = _getEstadoDeConservacion.GetById(especieVM.EstadoDeConservacionId);

                if (estadoDeConservacion == null)
                {
                    throw new Exception("No se encuentra el Estado de Conservacion");
                }

                List<Amenaza> amenazas = _getAmenazaSelected.GetSelected(especieVM.AmenazaIDs).ToList();


                if (!amenazas.Any())
                {
                    throw new Exception("No se encuentran las Amenazas de la especie");
                }

                List<Ecosistema> ecosistemasSeleccionados = _getSelectedEcosistema.GetSelected(especieVM.EcosistemaIDs).ToList();

                if (ecosistemasSeleccionados.Count() == 0)
                {
                    throw new Exception("No se encuentran los ecosistemas seleccionados");
                }

                var nuevaEspecie = new Especie
                {
                    //NombreCientifico = especieVM.NombreCientifico,
                    //NombreVulgar = especieVM.NombreVulgar,
                    Descripcion = especieVM.Descripcion,
                    //RangoPeso = new RangoPeso { Max = especieVM.PesoMax, Min = especieVM.PesoMin },
                    //RangoLargo = new RangoLargo { Max = especieVM.LargoMax, Min = especieVM.LargoMin },
                    EstadoConservacion = estadoDeConservacion,
                    Amenazas = amenazas,
                    PuedeHabitar = ecosistemasSeleccionados,
                };

                _create.Create(nuevaEspecie);
                List<ImagenEspecie> fotos = new List<ImagenEspecie>();
                int count = 1;

                foreach (var item in especieVM.ImagenesEspecies)
                {
                    ImagenEspecie img = new ImagenEspecie
                    { Nombre = Path.Combine($"/ImagenesEspecies/{nuevaEspecie.Id}_{count.ToString("000")}{Path.GetExtension(item.FileName)}") };
                    fotos.Add(img);

                    var rutaAbsoluta = Path.Combine($"{_webHostEnvironment.WebRootPath}\\ImagenesEspecies\\{nuevaEspecie.Id}_{count.ToString("000")}{Path.GetExtension(item.FileName)}");

                    FileStream foto = new FileStream(rutaAbsoluta, FileMode.Create);
                    item.CopyTo(foto);
                    count++;
                }

                nuevaEspecie.Imagenes = fotos;
                _update.Update(nuevaEspecie.Id, nuevaEspecie);

                Logger(nuevaEspecie.Id);

                ViewBag.TipoMensaje = "OK";
                ViewBag.Mensaje = "Especie creada correctamente.";

                var listaEspecies = _getAll.GetAll();
                return View("Index",MapeoListaEspecie(listaEspecies));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;

                especieVM.EstadosDeConservacion = _getAllEstados.GetAll();
                especieVM.Amenazas = _getAllAmenzas.GetAll();
                especieVM.PuedeHabitar = _getAllEcosistemas.GetAll();

                return View(especieVM);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Especie especie = _get.GetById((int)id);

            if (especie == null)
            {
                return RedirectToAction("Index");
            }
            return View(MapeoUnaEspecie(especie));
        }

        [Logueado("administrador", "persona")]
        [HttpGet]
        public IActionResult AsignarEcosistema(int id)
        {
            try
            {
                var esp = _get.GetById(id);

                return View(CargarComboAsignarEcosistema(esp));
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
        public IActionResult AsignarEcosistema(AsignarEcosistemaViewModel model)
        {
            try
            {
                var esp = _get.GetById(model.EspecieId);
                var eco = _getEcosistema.GetById(model.EcosistemaId);

                _addEcosistema.AddEcosistema(eco, esp);



                Logger(esp.Id);

                ViewBag.TipoMensaje = "OK";
                ViewBag.Mensaje = "Especie asignada correctamente.";

                return View("Index",MapeoListaEspecie(_getAll.GetAll()));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                var especie = _get.GetById(model.EspecieId);
                return View(CargarComboAsignarEcosistema(especie));
            }
        }

        [Logueado("administrador", "persona")]
        [HttpPost]
        public IActionResult DesasignarEcosistema(AsignarEcosistemaViewModel model)
        {
            try
            {
                var esp = _get.GetById(model.EspecieId);
                esp.Habita = null;

                _update.Update(esp.Id, esp);

                Logger(esp.Id);

                ViewBag.TipoMensaje = "OK";
                ViewBag.Mensaje = "Especie desvinculada del ecosistema correctamente.";

                var especies = _getAll.GetAll();
                return View("Index", MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                var especie = _get.GetById(model.EspecieId);
                return View(CargarComboAsignarEcosistema(especie));
            }
        }

        [HttpPost]
        public IActionResult FiltrarPorNombre(string NombreCientifico)
        {
            if (string.IsNullOrEmpty(NombreCientifico))
            {
                return RedirectToAction("index");
            }

            try
            {
                var especies = _getAllByString.GetAllByString(NombreCientifico).ToList();
                if (especies.Count() == 0)
                {
                    ViewBag.TipoMensaje = "ERROR";
                    ViewBag.Mensaje = "No hay especies con ese nombre cientifico";
                }

                return View("Index", MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }

        }

        [HttpGet]
        public IActionResult EspeciesEnPeligro()
        {
            try
            {
                var especies = _especiesPeligro.GetEspeciesPeligro();
                if (especies.Count() == 0)
                {
                    ViewBag.TipoMensaje = "ERROR";
                    ViewBag.Mensaje = "No hay especies en peligro de extincion";
                }
                return View("Index", MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }

        }

        [HttpPost]
        public IActionResult EspeciesPeso(int pesoMin, int pesoMax)
        {
            if(pesoMin == 0 || pesoMax == 0 )
                return RedirectToAction("Index");
            try
            {
                var especies = _listarPorpeso.GetEspeciePorPeso(pesoMin, pesoMax);
                if (especies.Count() == 0)
                {
                    ViewBag.TipoMensaje = "ERROR";
                    ViewBag.Mensaje = "No hay especies en ese rango de peso";
                }
                return View("Index", MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }

        }

        [HttpGet]
        public IActionResult NoPuedeHabitar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var ecosistemasNoHabita = _listarEcosistemas.EcosistemasQueNoHabitaLaEspecie((int)id);

                return View(ecosistemasNoHabita);
            }
            catch (Exception e)
            {

                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index");
            }

        }

        [HttpPost]
        public IActionResult FiltrarPorEcosistema(string NombreEcosistema)
        {
            if (string.IsNullOrEmpty(NombreEcosistema))
                return RedirectToAction("index");

            try
            {
                var especies = _getAllByEcosistema.GetAllByEcosistema(NombreEcosistema).ToList();


                if (especies.Count() == 0)
                {
                    ViewBag.TipoMensaje = "ERROR";
                    ViewBag.Mensaje = "No hay especies con ese nombre de Ecosistema";
                }
                return View("Index", MapeoListaEspecie(especies));
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Index", MapeoListaEspecie(_getAll.GetAll()));
            }

        }

        private IEnumerable<IndexEspecieViewModel> MapeoListaEspecie(IEnumerable<Especie> especies)
        {
            return especies.Select(e => new IndexEspecieViewModel 
            {
                    Id = e.Id,
                    NombreCientifico = e.NombreCientifico.Value,
                    NombreVulgar = e.NombreVulgar.Value,
                    Descripcion =e.Descripcion,
                    PuedeHabitar = e.PuedeHabitar,
                    Habita = e.Habita,
                    Imagenes = e.Imagenes,
                    RangoLargo = e.RangoLargo,
                    RangoPeso = e.RangoPeso,
                    Amenazas = e.Amenazas,
                    EstadoConservacion = e.EstadoConservacion
             }).ToList();
        }

        private IndexEspecieViewModel MapeoUnaEspecie(Especie especie)
        {
            return new IndexEspecieViewModel
            {
                Id = especie.Id,
                NombreCientifico = especie.NombreCientifico.Value,
                NombreVulgar = especie.NombreVulgar.Value,
                Descripcion = especie.Descripcion,
                PuedeHabitar = especie.PuedeHabitar,
                Habita = especie.Habita,
                Imagenes = especie.Imagenes,
                RangoLargo = especie.RangoLargo,
                RangoPeso = especie.RangoPeso,
                Amenazas = especie.Amenazas,
                EstadoConservacion = especie.EstadoConservacion
            };
        }

        private AsignarEcosistemaViewModel CargarComboAsignarEcosistema(Especie esp)
        {
            var model = new AsignarEcosistemaViewModel
            {
                EspecieId = esp.Id,
                Nombre = esp.NombreVulgar.Value,
                EcosistemasPosibles = esp.PuedeHabitar,
            };

            //if (esp.Habita != null)
                //model.EstaAsociada = esp.Habita.Nombre;

            return model;
        }

        private void Logger(int id)
        {
            var log = new Log
            {
                UserName = HttpContext.Session.GetString("alias"),
                IdEntidad = id,
                TipoEntidad = "Especie",
            };

            _log.Create(log);
        }
    }
}

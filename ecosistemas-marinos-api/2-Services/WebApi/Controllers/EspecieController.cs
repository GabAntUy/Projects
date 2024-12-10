using ApplicationLogic.Interfaces;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Especie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/especies")]
    [ApiController]
    public class EspecieController : ControllerBase
    {
        private ICreate<Especie> _create;
        private IGet<Especie> _get;
        private IGetAll<Especie> _getAll;
        private IUpdate<Especie> _update;
        private IGet<EstadoDeConservacion> _getEstadoDeConservacion;
        private IGetSelected<Amenaza> _getAmenazaSelected;
        private IGet<Ecosistema> _getEcosistema;
        private IAddEcosistema<Especie> _addEcosistema;
        private IGetSelected<Ecosistema> _getSelectedEcosistema;
        private IGetEspeciesPeligro<Especie> _especiesPeligro;
        private readonly IMapper _mapper;
        private ILogService _logService;


        //Consultas LINQ
        private IGetAllByString<Especie> _getAllByString;
        private IGetEspeciePorPeso<Especie> _listarPorpeso;
        private IListarEcosistema<Ecosistema> _listarEcosistemas;
        private IGetAllByEcosistema<Especie> _getAllByEcosistema;

        public EspecieController(
            ILogService logService,
            ICreate<Especie> create, 
            IGet<Especie> get, 
            IGetAll<Especie> getAll,
            IUpdate<Especie> update, 
            IGet<EstadoDeConservacion> getEstadoDeConservacion,
            IGetSelected<Amenaza> getAmenazaSelected, 
            IGet<Ecosistema> getEcosistema, 
            IGetAllByString<Especie> getAllByString, 
            IGetEspeciePorPeso<Especie> listar, 
            IAddEcosistema<Especie> addEcosistema, 
            IGetSelected<Ecosistema> getSelectedEcosistema,
            IListarEcosistema<Ecosistema> listarEcosistemas, 
            IGetAllByEcosistema<Especie> getAllByEcosistema, 
            IGetEspeciesPeligro<Especie> especiesPeligro,
            IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
            _create = create;
            _get = get;
            _getAll = getAll;
            _update = update;
            _getEstadoDeConservacion = getEstadoDeConservacion;
            _getAmenazaSelected = getAmenazaSelected;
            _getEcosistema = getEcosistema;
            _getAllByString = getAllByString;
            _listarPorpeso = listar;
            _addEcosistema = addEcosistema;
            _getSelectedEcosistema = getSelectedEcosistema;
            _listarEcosistemas = listarEcosistemas;
            _getAllByEcosistema = getAllByEcosistema;
            _especiesPeligro = especiesPeligro;
        }

       

        private ErrorDto CreateError(int code, string message, string details)
        {
            return new ErrorDto { Code = code, Message = message, Details = details };
        }
        
        /// <summary>
        /// Metodo que recibe una Id y retorno la especie correspondiente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)//es el equivalente a details
        {
            try
            {
                EspecieDto especie = _mapper.Map<EspecieDto>(_get.GetById(id));
                return Ok(especie);
            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);

                return NotFound(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Metodo que obtiene todos las especies
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Especie> especie = _getAll.GetAll();
                return Ok(_mapper.Map<IEnumerable<EspecieDto>>(especie));
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);

                return StatusCode(StatusCodes.Status204NoContent, err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }
        /// <summary>
        /// Metodo para registrar una nueva especie
        /// </summary>
        /// <param name="especie"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Create(CreateEspecieDto especie)
        {
            try
            {
                Especie esp = _mapper.Map<Especie>(especie, opts =>
                {
                    opts.Items["GetEcosistemasSelected"] = _getSelectedEcosistema;
                    opts.Items["GetAmenazaSelected"] = _getAmenazaSelected;
                    opts.Items["GetEstadoSelected"] = _getEstadoDeConservacion;

                });

                _create.Create(esp);
                _logService.CreateLog(esp.Id, "Especie");
                EspecieDto espDto = _mapper.Map<EspecieDto>(esp);

                return CreatedAtAction("GetById", new { id = espDto.Id }, espDto);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (AutoMapperMappingException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.GetBaseException().Message, e.GetBaseException().GetType().Name);

                return BadRequest(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Metodo que implementa una consulta LINQ para filtrar las especies por nombre cientifico
        /// </summary>
        /// <param name="NombreCientifico"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("FiltrarPorNombre")]
        //Carcharhinus limbatus
        public IActionResult FiltrarPorNombre(string NombreCientifico)
        {

            try
            {
                var especies = _getAllByString.GetAllByString(NombreCientifico).ToList();

                return Ok(_mapper.Map<IEnumerable<EspecieDto>>(especies));

            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);

                return NotFound(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }

        }
        /// <summary>
        /// Metodo que implementa una consulta LINQ para filtrar las especies en peligro
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("EspeciesEnPeligro")]

        public IActionResult EspeciesEnPeligro()
        {
            try
            {
               var especies = _especiesPeligro.GetEspeciesPeligro().ToList();
                return Ok(_mapper.Map<IEnumerable<EspecieDto>>(especies));
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);

                return StatusCode(StatusCodes.Status204NoContent, err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Metodo que implementa una consulta LINQ para filtrar las especies por un rago de peso( peso minimo y peso maximo)
        /// </summary>
        /// <param name="pesoMin"></param>
        /// <param name="pesoMax"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("EspeciesPeso")]

        public IActionResult EspeciesPeso(int pesoMin, int pesoMax)
        {

            try
            {
                var especies = _listarPorpeso.GetEspeciePorPeso(pesoMin, pesoMax);
                return Ok(_mapper.Map<IEnumerable<EspecieDto>>(especies));

            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);

                return NotFound(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }

        }

        /// <summary>
        /// Metodo que implementa una consulta LINQ para filtrar los ecosistemas que la especie no puede habitar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("NoPuedeHabitar")]

        public IActionResult NoPuedeHabitar(int? id)
        {
            try
            {
                var ecosistemasNoHabita = _listarEcosistemas.EcosistemasQueNoHabitaLaEspecie((int)id);
                return Ok(_mapper.Map<IEnumerable<EcosistemaDto>>(ecosistemasNoHabita));
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);

                return StatusCode(StatusCodes.Status204NoContent, err);
            }
            catch (RepositorioException e) 
            { 
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }

            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Metodo que implementa una consulta LINQ para filtrar las especies por el ecosistema que que habitan
        /// </summary>
        /// <param name="NombreEcosistema"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("FiltrarPorEcosistema")]

        public IActionResult FiltrarPorEcosistema(string NombreEcosistema)
        {
            try
            {
                var especies = _getAllByEcosistema.GetAllByEcosistema(NombreEcosistema).ToList();
                return Ok(_mapper.Map<IEnumerable<EspecieDto>>(especies));
            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);

                return StatusCode(StatusCodes.Status204NoContent, err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
          
        }

        /// <summary>
        /// Metodo Encargado de asignar un ecosistema con una especie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("AsignarEcosistema")]

        public IActionResult AsignarEcosistema(AsignarEcosistemaDto model)
        {
            try
            {

                var esp = _get.GetById(model.EspecieId);
                var eco = _getEcosistema.GetById(model.EcosistemaId);

                _addEcosistema.AddEcosistema(eco, esp);

                _logService.CreateLog(esp.Id, "Especie");

                return Ok();

            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);

                return NotFound(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Metodo Encargado de desasignar un ecosistema con una especie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("DesasignarEcosistema")]

        public IActionResult DesasignarEcosistema(AsignarEcosistemaDto model)
        {
            try
            {

                var esp = _get.GetById(model.EspecieId);
                esp.Habita = null;

                _update.Update(esp.Id, esp);

                _logService.CreateLog(esp.Id, "Especie");

                return Ok();

            }
            catch (RepositorioException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);

                return BadRequest(err);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);

                return NotFound(err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500, err);
            }
        }


    }

    }

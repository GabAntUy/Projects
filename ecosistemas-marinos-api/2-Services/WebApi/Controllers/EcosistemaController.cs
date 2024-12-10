using ApplicationLogic.Interfaces;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using DataAccessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Utils.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/ecosistemas")]
    [ApiController]
    public class EcosistemaController : ControllerBase
    {
        private ICreate<Ecosistema> _create;
        private IDelete<Ecosistema> _delete;
        private IGet<Ecosistema> _get;
        private IGetAll<Ecosistema> _getAll;
        private IGet<EstadoDeConservacion> _getEstadoPorId;
        private IGetSelected<Pais> _getPaisSelected;
        private IGetSelected<Amenaza> _getAmenazaSelected;
        private readonly IMapper _mapper;
        private ILogService _logService;

        public EcosistemaController(
            ILogService logService,
            ICreate<Ecosistema> create, 
            IDelete<Ecosistema> delete,
            IGet<Ecosistema> get, 
            IGetAll<Ecosistema> getAll, 
            IGet<EstadoDeConservacion> getEstadoPorId, 
            IGetSelected<Pais> getPaisSelected,
            IGetSelected<Amenaza> getAmenazaSelected,
            IMapper mapper
            )
            {
                _logService = logService;
                _mapper = mapper;
                _create = create;
                _delete = delete;
                _get = get;
                _getAll = getAll;
                _getEstadoPorId = getEstadoPorId;
                _getPaisSelected = getPaisSelected;
                _getAmenazaSelected = getAmenazaSelected;
            }

        /// <summary>
        /// Método que permite obtener un Ecosistema a partir de un Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id )
        {
            try
            {
                EcosistemaDto eco =  _mapper.Map<EcosistemaDto>(_get.GetById(id));
                return Ok(eco);
            }
            catch(RepositorioException e)
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
        /// Método que permite listar todos los Ecosistemas.
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
                IEnumerable<Ecosistema> ecos = _getAll.GetAll();
                return Ok(_mapper.Map<IEnumerable<EcosistemaDto>>(ecos));
            }
            catch(NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);

                return StatusCode(StatusCodes.Status204NoContent,err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);

                return StatusCode(500,err);
            }
        }

        /// <summary>
        /// Método que peermite borrar un ecosistema.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            try
            {
                _delete.Delete(id);
                _logService.CreateLog(id, "Ecosistema");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status404NotFound, e.Message, e.GetType().Name);
                return NotFound(err);
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
        /// Metodo que permite crear un Ecosistema.
        /// </summary>
        /// <param name="eco"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Create(CreateEcosistemaDto eco) 
        {
            try
            {
                Ecosistema e = _mapper.Map<Ecosistema>(eco, opts =>
                {
                    opts.Items["GetPaisSelected"] = _getPaisSelected;
                    opts.Items["GetAmenazaSelected"] = _getAmenazaSelected;
                    opts.Items["GetEstadoSelected"] = _getEstadoPorId;

                });

                _create.Create(e);
                _logService.CreateLog(e.Id, "Ecosistema");
                EcosistemaDto ecoDto = _mapper.Map<EcosistemaDto>(e);

                return CreatedAtAction("GetById", new { id = ecoDto.Id}, ecoDto);
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

        private ErrorDto CreateError(int code, string message,string details)
        {
            return new ErrorDto { Code = code, Message = message, Details = details };
        }

    }
}
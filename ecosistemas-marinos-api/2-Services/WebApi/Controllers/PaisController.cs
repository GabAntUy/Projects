using ApplicationLogic.Interfaces;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;
using DataAccessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/paises")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private IGetAll<Pais> _getAll;
        private readonly IMapper _mapper;

        public PaisController(IGetAll<Pais> getAll, IMapper mapper)
        {
            _getAll = getAll;
            _mapper = mapper;
        }

        /// <summary>
        /// Método que permite listar todos los paises.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Pais> paises = _getAll.GetAll();
                return Ok(_mapper.Map<IEnumerable<PaisDto>>(paises));
            }
            catch (NotFoundException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status204NoContent, e.Message, e.GetType().Name);
                return StatusCode(204,err);
            }
            catch (Exception e)
            {
                ErrorDto err = CreateError(StatusCodes.Status500InternalServerError, e.Message, e.GetType().Name);
                return StatusCode(500, err);
            }
        }
        private ErrorDto CreateError(int code, string message, string details)
        {
            return new ErrorDto { Code = code, Message = message, Details = details };
        }
    }
}

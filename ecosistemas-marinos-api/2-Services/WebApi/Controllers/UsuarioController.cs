using ApplicationLogic.Interfaces;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using DataAccessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Jwt;
using WebApi.Utils.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private ICreate<Usuario> _create;
        private ILogin<Usuario> _login;
        private IMapper _mapper;
        private ILogService _logService;

        public UsuarioController(
            ICreate<Usuario> create, 
            ILogin<Usuario> login, 
            ILogService logService, 
            IMapper mapper
            )
        {
            _logService = logService;
            _create = create;
            _login = login;
            _mapper = mapper;

        }

        /// <summary>
        /// Método encargado de crear usuarios.
        /// </summary>
        /// <remarks>
        /// {
        ///   "alias": "string",
        ///   "contrasenia": "string",
        ///   "contraseniaConfirmacion": "string"
        /// }
        /// </remarks>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RoleFilter("administrador")]
        [Route("create")]
        [HttpPost]
        public IActionResult Create(CreateUsuarioDto usuarioDto)
        {
            try
            {
                if (!usuarioDto.Contrasenia.Equals(usuarioDto.ContraseniaConfirmacion))
                {
                    return BadRequest("Las contraseñas no coinciden.");
                }

                Persona usuario = _mapper.Map<Persona>(usuarioDto);

                _create.Create(usuario);

                _logService.CreateLog(usuario.Id,"Usuario");

                UsuarioDto dto = _mapper.Map<UsuarioDto>(usuario);

                return StatusCode(201, dto);
            }
            catch (DomainException e)
            {
                ErrorDto err = CreateError(StatusCodes.Status400BadRequest, e.Message, e.GetType().Name);
                return BadRequest(err);
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
        /// El usuario se autentica brindado usuario y contraseña, si los mismos son correctos se devuelve un token jwt.
        /// </summary>
        /// <remarks>
        /// request: api/usuarios/login
        ///
        /// POST 
        /// {
        ///     {
        ///	        "alias" : "john@example.com",
        ///	        "password" : "MyPassword"
        ///	    }
        /// }
        ///
        /// </remarks>
        /// <param name="login"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [AllowAnonymous] //esto permite que se le pueda pegar a pesar de tener el authorize a nivel de todo el controller.
        [Route("login")]
        public IActionResult Login(LoginDto login)
        {

            try
            {
                Usuario usuario = _login.Login(login.Alias, login.Password);

                if (usuario != null)
                {
                    string rol = "persona";

                    if (usuario is Administrador)
                    {
                        rol = "administrador";
                    }
                    var token = JwtManager.GenerarToken(usuario, rol);

                    return Ok(new { Token = token, Rol = rol, Alias = usuario.Alias });
                }
                else
                {
                    return Unauthorized("Creenciales inválidas.");
                }
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

        private ErrorDto CreateError(int code, string message, string details)
        {
            return new ErrorDto { Code = code, Message = message, Details = details };
        }
    }
}

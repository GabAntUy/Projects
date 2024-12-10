using BusinessLogic.Entities;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Usuario;
using DataAccessLogic.Exceptions;
using BusinessLogic.RepoInterfaces;
using BusinessLogic.ApiDTO;
using DataAccessLogic.Exceptions.Especie;

namespace DataAccessLogic.Api
{
    public class RepositorioUsuarioApi :IRepositorioUsuario
    {
        private IHttpClientContext _context;
        private string _endpoint = "/api/usuarios/";
        private ManejoRespuestas _mr;
        public RepositorioUsuarioApi(IHttpClientContext context, ManejoRespuestas mr)
        {
            _context = context;
            _mr = mr;
        }

        public void Add(UsuarioApi obj)
        {
            try
            {
                var response = _context.Post(_endpoint + "create", obj);
                _mr.HandleResponse<UsuarioApi>(response);
            }
            catch (AggregateException)
            {
                throw new EspecieRepositorioException("Hubo un problema. Intenta mas tarde.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario? Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new UsuarioRepositorioException("Usuario o contraseña vacíos");
            }
            try
            {
                var response = _context.Post(_endpoint + "login", new { alias = username, password });

                if (response.IsSuccessStatusCode)
                {
                    TempUser user = _mr.HandleResponse<TempUser>(response); ;

                    if (user.Rol.Equals("administrador"))
                        return new Administrador { Alias = user.Alias, Token = user.Token, Role = user.Rol };

                    if (user.Rol.Equals("persona"))
                        return new Persona { Alias = user.Alias, Token = user.Token, Role = user.Rol };

                    throw new UsuarioRepositorioException();
                }
                else
                {
                    throw new HttpRequestException("Error al obtener amenazas seleccionadas: " + response.StatusCode);
                }
            }
            catch (AggregateException)
            {
                throw new EspecieRepositorioException("Hubo un problema. Intenta mas tarde.");
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }

        }
    }
    public class TempUser
    {
        public string Alias { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
    }
}

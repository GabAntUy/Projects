using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Especie;

namespace DataAccessLogic.Api
{
    public class RepositorioEstadoDeConservacionApi : IRepositorioEstadoDeConservacion
    {
        private IHttpClientContext _context;
        private string _endpoint = "/api/estados-de-conservacion/";
        private ManejoRespuestas _mr;
        public RepositorioEstadoDeConservacionApi(IHttpClientContext context, ManejoRespuestas mr)
        {
            _context = context;
            _mr = mr;
        }

        public IEnumerable<EstadoDeConservacion> GetAll()
        {
            try
            {
                var response = _context.Get(_endpoint);

                return _mr.HandleResponse<IEnumerable<EstadoDeConservacion>>(response);
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

        public EstadoDeConservacion GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

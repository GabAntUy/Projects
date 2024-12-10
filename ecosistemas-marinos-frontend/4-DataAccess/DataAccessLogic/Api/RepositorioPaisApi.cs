using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Especie;

namespace DataAccessLogic.Api
{
    public class RepositorioPaisApi : IRepositorioPais
    {
        private IHttpClientContext _context;
        private string _endpoint = "/api/paises/";
        private ManejoRespuestas _mr;

        public RepositorioPaisApi(IHttpClientContext context, ManejoRespuestas mr)
        {
            _context = context;
            _mr = mr;
        }

        public IEnumerable<Pais> GetAll()
        {
            try
            {
                var response = _context.Get(_endpoint);
                return _mr.HandleResponse<IEnumerable<Pais>>(response);
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
    }
}

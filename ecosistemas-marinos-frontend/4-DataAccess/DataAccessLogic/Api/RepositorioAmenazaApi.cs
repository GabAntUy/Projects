using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Especie;


namespace DataAccessLogic.Api
{
    public class RepositorioAmenazaApi : IRepositorioAmenaza
    {
        private ManejoRespuestas _mr;
        private IHttpClientContext _context;
        private string _endpoint = "/api/amenazas/";
        public RepositorioAmenazaApi(
            ManejoRespuestas mr, 
            IHttpClientContext context
            )
        {

            _mr = mr;
            _context = context;
        }

        public IEnumerable<Amenaza> GetAll()
        {
            try
            {
                var response = _context.Get(_endpoint);
                return _mr.HandleResponse<IEnumerable<Amenaza>>(response);
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

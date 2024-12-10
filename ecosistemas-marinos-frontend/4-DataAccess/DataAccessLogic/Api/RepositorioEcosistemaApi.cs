using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Especie;


namespace DataAccessLogic.Api
{
    public class RepositorioEcosistemaApi : IRepositorioEcosistema
    {
        private IHttpClientContext _context;
        private ManejoRespuestas _mr;
        private string _endpoint = "/api/ecosistemas/";

        public RepositorioEcosistemaApi(
            IHttpClientContext context, 
            ManejoRespuestas mr
            )
        {
            _context = context;
            _mr = mr;
        }

        public void Add(EcosistemaApi obj)
        {
 
            try
            {
                var response = _context.Post(_endpoint, obj);
                _mr.HandleResponse<Ecosistema>(response);
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

        public void Delete(int id)
        {
            try
            {
                var response = _context.Delete(_endpoint + id);
                _mr.HandleResponse<bool>(response);
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
        public IEnumerable<Ecosistema> EcosistemasQueNoHabitaLaEspecie(int Id)
        {
            try
            {
                var response = _context.Post("/api/especies/" + $"NoPuedeHabitar?id={Id}", new { });
                return _mr.HandleResponse<IEnumerable<Ecosistema>>(response);
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



        public IEnumerable<Ecosistema> GetAll()
        {
            try
            {
                var response = _context.Get(_endpoint);
                return _mr.HandleResponse<IEnumerable<Ecosistema>>(response);
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

        public Ecosistema GetById(int Id)
        {

            try
            {
                var response = _context.Get(_endpoint + Id);
                return _mr.HandleResponse<Ecosistema>(response);
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

        public IEnumerable<Ecosistema> GetSelectedById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void Update(Ecosistema obj, int id)
        {
            throw new NotImplementedException();
        }
    }
}

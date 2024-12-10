using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions.Especie;


namespace DataAccessLogic.Api
{
    public class RepositorioEspecieApi : IRepositorioEspecie
    {
        private IHttpClientContext _context;
        private ManejoRespuestas _mr;
        private string _endpoint = "api/especies/";

        public RepositorioEspecieApi(IHttpClientContext context, ManejoRespuestas mr)
        {
            _context = context;
            _mr = mr;
        }

        public void Add(EspecieApi obj)
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

        public void AsignarEcosistema(Ecosistema eco, Especie esp)
        {
            try
            {
                var response = _context.Post(_endpoint + "AsignarEcosistema", new { EcosistemaId=eco.Id,EspecieId = esp.Id });

                _mr.HandleResponse<Especie>(response);
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

        public IEnumerable<Especie> GetAll()
        {
            try
            {
                var response = _context.Get(_endpoint);
                return _mr.HandleResponse<IEnumerable<Especie>>(response);
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

        public IEnumerable<Especie> GetAllByEcosistema(string str)
        {
            try
            {
                var response = _context.Post(_endpoint + $"FiltrarPorEcosistema?NombreEcosistema={str}", new { });

                var esp = _mr.HandleResponse<IEnumerable<Especie>>(response);

                if (esp == null)
                {
                    throw new EspecieRepositorioException("No se encontraron especies para ese ecosistema.");
                }

                return esp;
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

        public IEnumerable<Especie> GetAllByString(string str)
        {
            try
            {
                var response = _context.Post(_endpoint + $"FiltrarPorNombre?NombreCientifico={str}", new { });

                var esp = _mr.HandleResponse<IEnumerable<Especie>>(response);

                if (esp == null)
                {
                    throw new EspecieRepositorioException("No se encontraron especies para ese nombre.");
                }

                return esp;
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

        public Especie GetById(int Id)
        {
            try
            {
                var response = _context.Get(_endpoint + Id);

                return _mr.HandleResponse<Especie>(response);
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

        public IEnumerable<Especie> GetEspeciesPeligro()
        {
            try
            {
                var response = _context.Get(_endpoint + "EspeciesEnPeligro");

                var especiesPeligro = _mr.HandleResponse<IEnumerable<Especie>>(response);

                if(especiesPeligro == null)
                {
                    throw new EspecieRepositorioException("No hoy especies en peligro.");
                }

                return especiesPeligro;
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

        public IEnumerable<Especie> GetEspeciesPeso(int pesoMin, int pesoMax)
        {
            try
            {
                var response = _context.Post(_endpoint + $"EspeciesPeso?pesoMin={pesoMin}&pesoMax={pesoMax}", new {});

                var esp = _mr.HandleResponse<IEnumerable<Especie>>(response);

                if (esp == null)
                {
                    throw new EspecieRepositorioException("No se encontraron especies en ese rango.");
                }
                return esp;
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

        public void Update(Especie obj, int id)
        {
            try
            {
                var response = _context.Post(_endpoint + "DesasignarEcosistema", new { EspecieId = id });

                _mr.HandleResponse<Especie>(response);
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

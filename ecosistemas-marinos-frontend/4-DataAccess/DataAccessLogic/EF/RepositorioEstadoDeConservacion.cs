using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using BusinessLogic.RepoInterfaces.IRepositorio;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.EstadoDeConservacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioEstadoDeConservacion : IRepositorioEstadoDeConservacion
    {
        private EcosistemasMarinosContext _context;
        private HttpClient _httpClient;
        public RepositorioEstadoDeConservacion(EcosistemasMarinosContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;

        }

        private IEnumerable<EstadoDeConservacion> Get()
        {
            var response = _httpClient.GetAsync("https://localhost:7259/api/estados-de-conservacion").Result;

            return response.Content.ReadFromJsonAsync<IEnumerable<EstadoDeConservacion>>().Result;

        }

        public IEnumerable<EstadoDeConservacion> GetAll()
        {
            try
            {
                return Get();
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema.");
            }
        }

        public EstadoDeConservacion? GetById(int Id)
        {
            if (Id == null || Id == 0)
                throw new EstadoDeConservacionRepositorioException("El valor recibido es nulo");
            try
            {
                return _context.EstadosDeConservacion
                    .Where(ec => ec.Id == Id)
                    .FirstOrDefault();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }
    }
}

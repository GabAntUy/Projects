using Azure;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Amenaza;
using DataAccessLogic.Exceptions.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioAmenaza : IRepositorioAmenaza
    {
        private EcosistemasMarinosContext _context;
        private HttpClient _httpClient;
        public RepositorioAmenaza(EcosistemasMarinosContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;

        }

        private IEnumerable<Amenaza> Get()
        {
            var response = _httpClient.GetAsync("https://localhost:7259/api/amenazas").Result;

            return response.Content.ReadFromJsonAsync<IEnumerable<Amenaza>>().Result;

            //return await _httpClient.GetFromJsonAsync<IEnumerable<Amenaza>>("https://localhost:7259/api/amenazas");
        }

        public IEnumerable<Amenaza> GetAll()
        {
            try
            {
                return Get();
                //return _context.Amenazas.ToList();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }

        public Amenaza? GetById(int id)
        {
            try
            {
                return _context.Amenazas.FirstOrDefault(a => a.Id == id);
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }

        public IEnumerable<Amenaza> GetSelectedById(IEnumerable<int> ids)
        {
            if (!ids.Any())
                throw new AmenazaRepositorioException("Se recibió una lista vacia");
            try
            {
                var selectedAmenazas = GetAll().Where(p => ids.Contains(p.Id)).ToList();

                if (selectedAmenazas.Count != ids.Distinct().Count())
                    throw new AmenazaRepositorioException("No se encontraron todos los elementos solicitados");

                return selectedAmenazas;
            }
            catch (AmenazaRepositorioException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema");
            }
        }
    }
}

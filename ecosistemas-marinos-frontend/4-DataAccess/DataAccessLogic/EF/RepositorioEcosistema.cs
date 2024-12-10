using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Api.Utilidades;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Amenaza;
using DataAccessLogic.Exceptions.Ecosistema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioEcosistema 
    {
        private EcosistemasMarinosContext _context;
        private readonly HttpClient _httpClient;
        private ManejoRespuestas _mr;

        public RepositorioEcosistema(EcosistemasMarinosContext context, HttpClient httpClient, ManejoRespuestas mr)
        {
            _context = context;
            _httpClient = httpClient;
            _mr = mr;

        }

        public void Add(Ecosistema obj)
        {
            if (obj == null)
                throw new EcosistemaRepositorioException("No se recibio ningun objeto.");

            obj.Validar();

            try
            {
                _context.Ecosistemas.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new InfraException();
            }
        }

        public void Delete(int id)
        {
            Ecosistema eco = GetById(id);
            if (eco == null)
                throw new EcosistemaRepositorioException("No se encontró el ecosistema.");

            if (eco.EspeciesQueLoHabitan.Any())
                throw new EcosistemaRepositorioException("El repositorio tiene especies asociadas");

            eco.EstaActivo = false;
            Update(eco, id);
        }

        public IEnumerable<Ecosistema> EcosistemasQueNoHabitaLaEspecie(int Id)
        {
            var especie = _context.Especies.FirstOrDefault(e => e.Id == Id);

            var ecosistemas = _context.Ecosistemas
            .Where(ecosistema => !ecosistema.EspeciesQuePuedenHabitarlo.Contains(especie))
            .ToList();

            return ecosistemas;
        }

        private IEnumerable<Ecosistema> Get()
        {
            var response = _httpClient.GetAsync("https://localhost:7259/api/ecosistemas").Result;

            return response.Content.ReadFromJsonAsync<IEnumerable<Ecosistema>>().Result;

        }

        public IEnumerable<Ecosistema> GetAll()
        {
            try
            {
                return Get();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }



        public Ecosistema? GetById(int Id)
        {
            if (Id == 0)
                throw new EcosistemaRepositorioException("No se encontro el elemento.");
            try
            {
                //return _context.Ecosistemas
                //    .Include(eco => eco.Amenazas)
                //    .Include(eco => eco.EstadoDeConservacion)
                //    .Include(eco => eco.Ubicacion)
                //    .Include(eco => eco.Paises)
                //    .Include(eco => eco.EspeciesQueLoHabitan)
                //    .FirstOrDefault(eco => eco.Id == Id);

                var response = _httpClient.GetAsync($"https://localhost:7259/api/ecosistemas/{Id}").Result;
                return _mr.HandleResponse<Ecosistema>(response);               
            }
            catch (Exception)
            {
                throw;
                //throw new InfraException("Hubo un problema");
            }
        }

        public IEnumerable<Ecosistema> GetSelectedById(IEnumerable<int> ids)
        {
            if (!ids.Any())
                throw new EcosistemaRepositorioException("Se recibió una lista vacia");
            try
            {
                var selectedEcosistemas = GetAll().Where(p => ids.Contains(p.Id)).ToList();

                if (selectedEcosistemas.Count != ids.Distinct().Count())
                    throw new EcosistemaRepositorioException("No se encontraron todos los elementos solicitados");

                return selectedEcosistemas;
            }
            catch (EcosistemaRepositorioException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema");
            }
        }

        public void Update(Ecosistema obj, int id)
        {
            if (obj == null || id == 0)
            {
                throw new EcosistemaRepositorioException("No se recibio el Ecosistema");
            }
            Ecosistema eco = GetById(id);
            if (eco == null)
            {
                throw new EcosistemaRepositorioException("No se recibio el Ecosistema");
            }
            try
            {
                eco.Update(obj);
                _context.Ecosistemas.Update(eco);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }
    }
}

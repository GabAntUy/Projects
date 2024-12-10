using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Amenaza;
using DataAccessLogic.Exceptions.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioAmenaza : IRepositorioAmenaza
    {
        private EcosistemasMarinosContext _context;
        public RepositorioAmenaza(EcosistemasMarinosContext context)
        {
            _context = context;
        }

        public IEnumerable<Amenaza> GetAll()
        {
            try
            {
                var amenazas = _context.Amenazas.ToList();
                if (!amenazas.Any())
                {
                    throw new NotFoundException("No se encontraron Amenazas");
                }
                return amenazas;
            }
            catch (NotFoundException)
            {
                throw;
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
                Amenaza a = _context.Amenazas.FirstOrDefault(a => a.Id == id);

                if (a == null)
                {
                    throw new NotFoundException($"No se encontro la amenaza para el Id {id}");
                }
                return a;
            }
            catch (NotFoundException)
            {
                throw;
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

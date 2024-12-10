using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Amenaza;
using DataAccessLogic.Exceptions.Ecosistema;
using DataAccessLogic.Exceptions.Especie;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLogic.EF
{
    public class RepositorioEcosistema : IRepositorioEcosistema
    {
        private EcosistemasMarinosContext _context;
        public RepositorioEcosistema(EcosistemasMarinosContext context)
        {
            _context = context;
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
                throw new NotFoundException("No se encontró el ecosistema.");

            if (eco.EspeciesQueLoHabitan.Any())
                throw new EcosistemaRepositorioException("El Ecosistema tiene especies asociadas");

            eco.EstaActivo = false;
            Update(eco, id);
        }

        public IEnumerable<Ecosistema> EcosistemasQueNoHabitaLaEspecie(int Id)
        {
            try
            {
                var especie = _context.Especies.FirstOrDefault(e => e.Id == Id);

                var ecosistemas = _context.Ecosistemas
                .Where(ecosistema => !ecosistema.EspeciesQuePuedenHabitarlo.Contains(especie))
                .ToList();

                if (especie == null)
                    throw new RepositorioException("No se encontro especie");
                if (!ecosistemas.Any())
                    throw new NotFoundException("No se encontraron ecosistemas que la especie no habite");
                return ecosistemas;

            }
            catch (NotFoundException)
            {
                throw;
            }

            catch (RepositorioException)
            {
                throw;
            }

            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }


        }

        public IEnumerable<Ecosistema> GetAll()
        {
            try
            {
                var ecos = _context.Ecosistemas.ToList();

                if (!ecos.Any())
                {
                    throw new NotFoundException("No hay ecosistemas registrados.");
                }

                return ecos;
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

        public Ecosistema GetById(int Id)
        {
            if (Id == 0)
                throw new EcosistemaRepositorioException("No se encontro el elemento.");

            try
            {
                Ecosistema? eco = _context.Ecosistemas
                                 .Include(eco => eco.Amenazas)
                                 .Include(eco => eco.EstadoDeConservacion)
                                 .Include(eco => eco.Ubicacion)
                                 .Include(eco => eco.Paises)
                                 .Include(eco => eco.EspeciesQueLoHabitan)
                                 .FirstOrDefault(eco => eco.Id == Id);
                if (eco == null)
                    throw new NotFoundException("No se encontro el Ecosistema con el id especificado.");

                return eco;
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
                throw new NotFoundException("No se encontró el Ecosistema");
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

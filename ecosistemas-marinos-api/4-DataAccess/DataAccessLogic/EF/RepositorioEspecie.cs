using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions.Usuario;
using DataAccessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLogic.Exceptions.Especie;
using DataAccessLogic.Exceptions.Ecosistema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLogic.EF
{
    public class RepositorioEspecie : IRepositorioEspecie
    {
        private EcosistemasMarinosContext _context;

        public RepositorioEspecie(EcosistemasMarinosContext context)
        {
            _context = context;
        }


        public void Add(Especie obj)
        {
            if (obj == null) throw new EspecieRepositorioException("El objeto es null");

            obj.Validar();

            try
            {
                _context.Especies.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }

        public void AsignarEcosistema(Ecosistema eco, Especie esp)
        {
            try
            {
                if (esp == null)
                    throw new EspecieRepositorioException("La especie recibida es nulo.");

                if (eco == null)
                    throw new EspecieRepositorioException("El ecosistema no puede ser nulo.");

                //esp.Validar();
                //eco.Validar();

                if (esp.Habita != null)
                    throw new EspecieRepositorioException("La especie ya se encuentra asociada a un ecosistema.");

                if (esp.Amenazas.All(a => eco.Amenazas.Contains(a)) && esp.Amenazas.Count == eco.Amenazas.Count)
                    throw new EspecieRepositorioException("La especie y el ecosistema contienen las mismas amenazas.");

                if (eco.EstadoDeConservacion.RangoConservacion.Minimo < esp.EstadoConservacion.RangoConservacion.Minimo)
                    throw new EspecieRepositorioException("El estado de conservacion del ecosistema es peor que el de la especie.");

                esp.Habita = eco;

                Update(esp, esp.Id);
            }
            catch (EspecieRepositorioException)
            {
                throw;
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema.");
            }
        }

        public IEnumerable<Especie> GetAll()
        {
            try
            {
                var especies = _context.Especies.ToList();
                if (!especies.Any())
                {
                    throw new NotFoundException("No se encontraron Especies");
                }

                return especies;
            }
            catch(NotFoundException) { throw; }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }

        }



        public Especie GetById(int id)
        {
            if(id == null || id == 0)
                throw new EspecieRepositorioException("no se admiten atributos nulos.");
            try
            {
                var especie = _context.Especies
                    .Include(e => e.PuedeHabitar)
                    .Include(e => e.Amenazas)
                    .Include(e => e.RangoPeso)
                    .Include(e => e.RangoLargo)
                    .Include(e => e.EstadoConservacion)
                    .Include(e => e.Habita)
                    .ThenInclude(habita => habita.Imagenes)
                    .FirstOrDefault(e => e.Id == id);
                if(especie == null)
                {
                    throw new NotFoundException($"No se encontro la especie con el Id {id}");
                }
                return especie;
            }
            catch (NotFoundException) { throw; }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema.");
            }

        }

        public void Update(Especie obj, int id)
        {
            if (obj == null || id == 0)
            {
                throw new EspecieRepositorioException("No se recibio la Especie");
            }

            Especie especie = GetById(id);

            if (especie == null)
            {
                throw new EspecieRepositorioException("No se recibio el Especie");
            }

            try
            {
                especie.Update(obj);
                _context.Especies.Update(especie);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }


        public IEnumerable<Especie> GetAllByString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new EspecieRepositorioException("No se recibio un parametro de busqueda");
            try
            {
                var especies = _context.Especies
                             //.Include(e => e.PuedeHabitar)
                             .Include(e => e.Amenazas)
                             .Include(e => e.RangoPeso)
                             .Include(e => e.RangoLargo)
                             .Include(e => e.EstadoConservacion)
                             //.Include(e => e.Habita)
                             //.ThenInclude(habita => habita.Imagenes)
                             .AsEnumerable()
                             .Where(e => e.NombreCientifico.Value.ToLower() == str.ToLower());

                if (!especies.Any())
                    throw new NotFoundException("No se encontraron especies con ese nombre cientifico");
                return especies;
            }
            catch (NotFoundException){

                throw;
            
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema.");
            }
        }

        public IEnumerable<Especie> GetEspeciesPeligro()
        {
            
            try
            {
                var especies = _context.Especies
                    //.Include(e => e.PuedeHabitar)
                    .Include(e => e.Amenazas)
                    .Include(e => e.RangoPeso)
                    .Include(e => e.RangoLargo)
                    .Include(e => e.EstadoConservacion)
                    .Include(e => e.Habita)
                    .ThenInclude(habita => habita.Imagenes)
                    .Where(e =>
                         e.EstadoConservacion.RangoConservacion.Minimo < 60 ||
                         e.Amenazas.Count() > 3 ||
                         (e.Habita != null &&
                         (e.Habita.Amenazas.Count() > 3 &&
                         e.Habita.EstadoDeConservacion.RangoConservacion.Minimo < 60)))
                    .ToList();

                return especies;
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema.");
            }
        }

        public IEnumerable<Especie> GetEspeciesPeso(int pesoMin, int pesoMax)
        {
            if (pesoMax == 0)
                throw new EspecieRepositorioException("El Peso maximo no puede ser cero o vacio");

            if (pesoMin == null || pesoMax == null)
                throw new EspecieRepositorioException("No se recibio un parametro de busqueda");

            if (pesoMax < pesoMin)
                throw new EspecieRepositorioException("El paso maximo no puede ser menor que el minimo");

            try
            {
                var especies = _context.Especies
                    .Where(e => e.RangoPeso.Min >= pesoMin
                     && e.RangoPeso.Max <= pesoMax)
                    .ToList();
                if (!especies.Any())
                    throw new NotFoundException("No se encontraron especies en ese rango de peso");
                return especies;
            }
            catch (NotFoundException)
            {

                throw;

            }
            
            catch (Exception)
            {
                throw new InfraException("Hubo un problema.");
            }
        }

        public IEnumerable<Especie> GetAllByEcosistema(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new EspecieRepositorioException("No se recibio un parametro de busqueda");
            try
            {
                //var especies = _context.Especies
                //    .Where(esp => esp.Habita.Nombre.Value.ToLower() == str.ToLower())
                //    .ToList();
                var especiesConHabita = _context.Especies
                                        .Include(esp => esp.Habita)
                                        .ToList();

                var especiesFiltradas = especiesConHabita
                                        .Where(esp => esp.Habita != null && esp.Habita.Nombre.Value.ToLower() == str.ToLower())
                                        .ToList();

                if (!especiesFiltradas.Any())
                {
                    throw new NotFoundException("No se encontraron Especies para ese nombre de ecosistema.");
                }

                return especiesFiltradas;
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema.");
            }

            

        }
    }
}

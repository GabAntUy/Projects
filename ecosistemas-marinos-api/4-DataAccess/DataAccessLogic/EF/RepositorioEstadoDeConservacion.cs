using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using BusinessLogic.RepoInterfaces.IRepositorio;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.EstadoDeConservacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioEstadoDeConservacion : IRepositorioEstadoDeConservacion
    {
        private EcosistemasMarinosContext _context;
        public RepositorioEstadoDeConservacion(EcosistemasMarinosContext context)
        {
            _context = context;
        }
        public IEnumerable<EstadoDeConservacion> GetAll()
        {
            try
            {
                var estados = _context.EstadosDeConservacion.ToList();
                if (!estados.Any())
                {
                    throw new NotFoundException("No se encontraron Estados de Conservacion");
                }
                return estados;
            }
            catch (NotFoundException) { throw; }
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
                var estado = _context.EstadosDeConservacion
                    .Where(ec => ec.Id == Id)
                    .FirstOrDefault();
                if(estado == null)
                {
                    throw new NotFoundException("No se encontró el Estado de Conservación.");
                }
                return estado;
            }
            catch (NotFoundException) { throw; }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }
    }
}

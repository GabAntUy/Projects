using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioPais : IRepositorioPais
    {
		private EcosistemasMarinosContext _context;
        public RepositorioPais(EcosistemasMarinosContext context)
        {
            _context = context;
        }
        public IEnumerable<Pais> GetAll()
        {
			try
			{
				var paises = _context.Paises.ToList();
				if(!paises.Any())
				{
					throw new NotFoundException("No se encontraron paises");
				}
				return paises;
			}
			catch (Exception)
			{
				throw new InfraException("Hubo un problema.");
			}
        }

        public IEnumerable<Pais> GetSelectedById(IEnumerable<int> ids)
        {
			if (!ids.Any())
				throw new PaisRepositorioException("Se recibió una lista vacia");
			try
			{
				var selectedPaises = GetAll().Where(p => ids.Contains(p.Id)).ToList();

				if (selectedPaises.Count != ids.Distinct().Count())
					throw new PaisRepositorioException("No se encontraron todos los elementos solicitados");

				return selectedPaises;
			}
			catch (PaisRepositorioException)
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

using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioLog : IRepositorioLog
    {
        private EcosistemasMarinosContext _context;
        public RepositorioLog(EcosistemasMarinosContext context)
        { 
            _context = context;
        }

        public void Add(Log obj)
        {
            if (obj.Equals(null))
                throw new LogRepositorioException("Se recibió un log nulo");
            obj.Validar();
            try
            {
                _context.Logs.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new InfraException("Hubo un problema.");
            }
        }
    }
}

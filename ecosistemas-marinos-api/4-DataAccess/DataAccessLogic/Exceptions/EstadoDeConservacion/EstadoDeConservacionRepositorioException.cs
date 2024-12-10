using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.EstadoDeConservacion
{
    public class EstadoDeConservacionRepositorioException : RepositorioException
    {
        public EstadoDeConservacionRepositorioException()
        {
            
        }

        public EstadoDeConservacionRepositorioException(string? message) : base(message)
        {
        }
    }
}

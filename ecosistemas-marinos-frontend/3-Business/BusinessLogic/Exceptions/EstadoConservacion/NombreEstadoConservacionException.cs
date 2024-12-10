using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.EstadoConservacion
{
    public class NombreEstadoConservacionException : EstadoConservacionException
    {
        public NombreEstadoConservacionException()
        {
        }

        public NombreEstadoConservacionException(string? message) : base(message)
        {
        }
    }
}

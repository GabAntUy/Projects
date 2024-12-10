using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.EstadoConservacion
{
    public class EstadoConservacionException : DomainException
    {
        public EstadoConservacionException()
        {
        }

        public EstadoConservacionException(string? message) : base(message)
        {
        }
    }
}

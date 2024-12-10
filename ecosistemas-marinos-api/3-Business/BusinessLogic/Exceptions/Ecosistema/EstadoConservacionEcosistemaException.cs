using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class EstadoConservacionEcosistemaException : EcosistemaException
    {
        public EstadoConservacionEcosistemaException()
        {
        }

        public EstadoConservacionEcosistemaException(string? message) : base(message)
        {
        }

    }
}

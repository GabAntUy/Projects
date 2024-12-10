using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class AmenazasEcosistemaException : EcosistemaException
    {
        public AmenazasEcosistemaException()
        {
        }

        public AmenazasEcosistemaException(string? message) : base(message)
        {
        }
    }
}

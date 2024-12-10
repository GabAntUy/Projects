using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class NombreEcosistemaException :EcosistemaException
    {
        public NombreEcosistemaException()
        {
            
        }

        public NombreEcosistemaException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class UbicacionEcosistemaException : EcosistemaException
    {
        public UbicacionEcosistemaException(string? message) : base(message)
        {
        }
        public UbicacionEcosistemaException()
        {
            
        }
    }
}

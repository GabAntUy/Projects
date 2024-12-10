using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class DescripcionEcosistemaException : EcosistemaException
    {
        public DescripcionEcosistemaException()
        {
            
        }

        public DescripcionEcosistemaException(string? message) : base(message)
        {
        }
    }
}

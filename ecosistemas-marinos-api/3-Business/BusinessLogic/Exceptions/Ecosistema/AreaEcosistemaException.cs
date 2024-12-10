using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class AreaEcosistemaException : EcosistemaException
    {
        public AreaEcosistemaException()
        {
            
        }

        public AreaEcosistemaException(string? message) : base(message)
        {
        }
    }
}

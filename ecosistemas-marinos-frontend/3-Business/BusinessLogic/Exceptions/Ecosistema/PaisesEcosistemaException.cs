using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class PaisesEcosistemaException : EcosistemaException
    {
        public PaisesEcosistemaException()
        {
        }

        public PaisesEcosistemaException(string? message) : base(message)
        {
        }

    }
}

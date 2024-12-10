using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Pais
{
    public class NombrePaisException :PaisException
    {
        public NombrePaisException() { }

        public NombrePaisException(string? message) : base(message)
        {
        }
    }
}

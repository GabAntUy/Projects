using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ubicacion
{
    public class UbicacionException : Exception
    {
        public UbicacionException() { }

        public UbicacionException(string? message) : base(message)
        {
        }
    }
}

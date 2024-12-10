using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ubicacion
{
    public class LongitudUbicacionException :UbicacionException
    {
        public LongitudUbicacionException()
        {
            
        }

        public LongitudUbicacionException(string? message) : base(message)
        {
        }
    }
}

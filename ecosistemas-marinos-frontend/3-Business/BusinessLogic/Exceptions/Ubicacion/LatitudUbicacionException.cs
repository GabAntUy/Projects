using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ubicacion
{
    public class LatitudUbicacionException :UbicacionException
    {
        public LatitudUbicacionException()
        {
            
        }

        public LatitudUbicacionException(string? message) : base(message)
        {
        }
    }
}

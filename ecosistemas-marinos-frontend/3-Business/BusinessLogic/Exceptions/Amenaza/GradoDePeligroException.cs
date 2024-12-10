using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Amenaza
{
    public class GradoDePeligroException : AmenazaException
    {
        public GradoDePeligroException()
        {
            
        }

        public GradoDePeligroException(string? message) : base(message)
        {
        }
    }
}

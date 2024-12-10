using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Amenaza
{
    public class DescripcionAmenazaException : AmenazaException
    {
        public DescripcionAmenazaException()
        {
            
        }

        public DescripcionAmenazaException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Amenaza
{
    public class AmenazaException : DomainException
    {
        public AmenazaException()
        {
            
        }

        public AmenazaException(string? message) : base(message)
        {
        }
    }
}

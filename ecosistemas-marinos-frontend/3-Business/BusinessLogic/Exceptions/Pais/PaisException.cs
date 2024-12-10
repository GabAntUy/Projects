using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Pais
{
    public class PaisException : DomainException
    {
        public PaisException()
        {
            
        }

        public PaisException(string? message) : base(message)
        {
        }
    }
}

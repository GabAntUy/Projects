using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Nombre
{
    public class NombreException : DomainException
    {
        public NombreException() { }

        public NombreException(string? message) : base(message)
        {
        }
    }
}

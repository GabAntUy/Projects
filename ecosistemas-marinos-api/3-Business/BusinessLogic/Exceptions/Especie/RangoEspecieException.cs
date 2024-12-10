using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Especie
{
    public class RangoEspecieException : EspecieException
    {
        public RangoEspecieException()
        {
        }

        public RangoEspecieException(string? message) : base(message)
        {
        }
    }
}

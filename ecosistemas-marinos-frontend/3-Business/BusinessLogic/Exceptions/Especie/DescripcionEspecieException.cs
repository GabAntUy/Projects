using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Especie
{
    public class DescripcionEspecieException :EspecieException
    {
        public DescripcionEspecieException()
        {
            
        }

        public DescripcionEspecieException(string? message) : base(message)
        {
        }
    }
}

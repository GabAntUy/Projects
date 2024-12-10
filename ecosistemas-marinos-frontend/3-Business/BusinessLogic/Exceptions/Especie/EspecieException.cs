using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Especie
{
    public class EspecieException :Exception
    {
        public EspecieException() { }

        public EspecieException(string? message) : base(message)
        {
        }
    }
}

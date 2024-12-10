using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Especie
{
    public class NombreVulgarEspecieException :EspecieException
    {
        public NombreVulgarEspecieException()
        {
            
        }

        public NombreVulgarEspecieException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Especie
{
    public class NombreCientificoEspecieException : EspecieException
    {
        public NombreCientificoEspecieException()
        {
            
        }

        public NombreCientificoEspecieException(string? message) : base(message)
        {
        }
    }
}

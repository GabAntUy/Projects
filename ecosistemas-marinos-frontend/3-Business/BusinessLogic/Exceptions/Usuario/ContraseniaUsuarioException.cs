using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Usuario
{
    public class ContraseniaUsuarioException : UsuarioException
    {
        public ContraseniaUsuarioException()
        {
        }

        public ContraseniaUsuarioException(string? message) : base(message)
        {
        }
    }
}

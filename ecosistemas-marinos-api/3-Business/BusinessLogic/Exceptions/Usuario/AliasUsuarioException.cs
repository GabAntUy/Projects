using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Usuario
{
    public class AliasUsuarioException : UsuarioException
    {
        public AliasUsuarioException()
        {
        }

        public AliasUsuarioException(string? message) : base(message)
        {
        }
    }
}

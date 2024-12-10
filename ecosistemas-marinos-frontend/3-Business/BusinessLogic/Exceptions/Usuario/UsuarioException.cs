using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Usuario
{
    public class UsuarioException :DomainException
    {
        public UsuarioException() { }

        public UsuarioException(string? message) : base(message)
        {
        }
    }
}

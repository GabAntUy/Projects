using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.Usuario
{
    public class UsuarioRepositorioException : RepositorioException
    {
        public UsuarioRepositorioException()
        {
            
        }

        public UsuarioRepositorioException(string? message) : base(message)
        {
        }
    }
}

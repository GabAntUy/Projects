using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.Pais
{
    public class PaisRepositorioException : RepositorioException
    {
        public PaisRepositorioException()
        {
        }

        public PaisRepositorioException(string? message) : base(message)
        {
        }
    }
}

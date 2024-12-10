using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.Especie
{
    public class EspecieRepositorioException : RepositorioException
    {
        public EspecieRepositorioException()
        {

        }

        public EspecieRepositorioException(string? message) : base(message)
        {
        }
    }
}

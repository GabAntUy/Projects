using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions
{
    public class RepositorioException :Exception
    {
        public RepositorioException() { }

        public RepositorioException(string? message) : base(message)
        {
        }
    }
}

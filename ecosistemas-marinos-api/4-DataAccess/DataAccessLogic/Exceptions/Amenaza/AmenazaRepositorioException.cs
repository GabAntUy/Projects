using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.Amenaza
{
    public class AmenazaRepositorioException : RepositorioException
    {
        public AmenazaRepositorioException()
        {
            
        }

        public AmenazaRepositorioException(string? message) : base(message)
        {
        }
    }
}

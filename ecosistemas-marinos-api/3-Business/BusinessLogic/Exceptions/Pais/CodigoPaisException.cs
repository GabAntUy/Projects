using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Pais
{
    public class CodigoPaisException :PaisException
    {
        public CodigoPaisException()
        {
            
        }

        public CodigoPaisException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions
{
    public class InfraException : Exception
    {
        public InfraException()
        {
            
        }

        public InfraException(string? message) : base(message)
        {
        }
    }
}

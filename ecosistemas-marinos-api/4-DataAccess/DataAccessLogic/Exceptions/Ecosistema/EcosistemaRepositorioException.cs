using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Exceptions.Ecosistema
{
    public class EcosistemaRepositorioException :RepositorioException
    {
        public EcosistemaRepositorioException()
        {
            
        }

        public EcosistemaRepositorioException(string? message) : base(message)
        {
        }
    }
}

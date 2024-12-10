using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class ImagenesEcosistemaException : EcosistemaException
    {
        public ImagenesEcosistemaException()
        {
            
        }

        public ImagenesEcosistemaException(string? message) : base(message)
        {
        }
    }
}

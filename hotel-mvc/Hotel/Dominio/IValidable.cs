using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Interface pública IValidable, es utilizada para obligar a las clases que la implementen a tener un método public void Validar()
    /// </summary>
    public interface IValidable
    {
        public void Validar();
    }
}

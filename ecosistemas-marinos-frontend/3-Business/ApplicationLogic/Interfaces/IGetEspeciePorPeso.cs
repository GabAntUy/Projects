using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IGetEspeciePorPeso<T>
    {
        public IEnumerable<T> GetEspeciePorPeso(int pesoMin, int pesoMax);
    }
}

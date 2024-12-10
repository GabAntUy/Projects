using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IGetEspeciesPeligro<T>
    {
        public IEnumerable<T> GetEspeciesPeligro();
    }
}

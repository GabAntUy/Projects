using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IGetSelected<T>
    {
        public IEnumerable<T> GetSelected(IEnumerable<int> ids);
    }
}

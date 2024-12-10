using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IGetAllById<T>
    {
       
        public IEnumerable<T> GetAllById(List<int> IDs);
    }
}

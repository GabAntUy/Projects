using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces.IRepositorio
{
    public interface IGetAllRepositorio<T>
    {
        public IEnumerable<T> GetAll();
    }
}

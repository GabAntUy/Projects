using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces.IRepositorio
{
    public interface IGetAllByStringRepositorio<T>
    {
        public IEnumerable<T> GetAllByString(string str);
    }
}

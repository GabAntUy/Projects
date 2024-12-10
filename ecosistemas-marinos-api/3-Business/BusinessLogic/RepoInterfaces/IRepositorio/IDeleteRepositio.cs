using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces.IRepositorio
{
    public interface IDeleteRepositio<T>
    {
        public void Delete(int id);
    }
}

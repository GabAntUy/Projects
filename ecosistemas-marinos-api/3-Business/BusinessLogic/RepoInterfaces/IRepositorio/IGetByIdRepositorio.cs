using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces.IRepositorio
{
    public interface IGetByIdRepositorio<T>
    {
        public T GetById(int Id);
    }
}

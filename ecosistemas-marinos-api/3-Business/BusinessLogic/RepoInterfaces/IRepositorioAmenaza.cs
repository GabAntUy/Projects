using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces
{
    public interface IRepositorioAmenaza : IGetAllRepositorio<Amenaza>, IGetByIdRepositorio<Amenaza>, IGetSelectedByIdRepositorio<Amenaza>
    {
    }
}

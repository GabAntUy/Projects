using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces
{
    public interface IRepositorioEcosistema : IAddRepositorio<EcosistemaApi>,
        IGetAllRepositorio<Ecosistema>, IGetByIdRepositorio<Ecosistema>, IUpdateRepositorio<Ecosistema>,
        IDeleteRepositio<Ecosistema>, IGetSelectedByIdRepositorio<Ecosistema>
    {
        IEnumerable<Ecosistema> EcosistemasQueNoHabitaLaEspecie(int Id);
    }
}

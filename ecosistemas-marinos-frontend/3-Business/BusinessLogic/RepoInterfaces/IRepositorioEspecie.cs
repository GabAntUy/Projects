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
    public interface IRepositorioEspecie : IAddRepositorio<EspecieApi>, IUpdateRepositorio<Especie>,
        IGetAllRepositorio<Especie>, IGetByIdRepositorio<Especie>, IGetAllByStringRepositorio<Especie>
    {
       public IEnumerable<Especie> GetEspeciesPeligro();
       public IEnumerable<Especie> GetEspeciesPeso(int pesoMin, int pesoMax);

       public void AsignarEcosistema(Ecosistema eco, Especie esp);
       public IEnumerable<Especie> GetAllByEcosistema(string str);


    }
}

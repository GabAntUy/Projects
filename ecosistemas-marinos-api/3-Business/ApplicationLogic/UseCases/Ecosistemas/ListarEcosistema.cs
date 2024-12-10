using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Ecosistemas
{
    public class ListarEcosistema : IGet<Ecosistema>, IGetAll<Ecosistema>, IGetSelected<Ecosistema>, IListarEcosistema<Ecosistema>
    {
        private IRepositorioEcosistema _repo;
        public ListarEcosistema(IRepositorioEcosistema repo)
        {
            _repo = repo;
        }

        public IEnumerable<Ecosistema> EcosistemasQueNoHabitaLaEspecie(int id)
        {
            return _repo.EcosistemasQueNoHabitaLaEspecie(id);
        }

        public IEnumerable<Ecosistema> GetAll()
        {
            return _repo.GetAll();
        }

        public Ecosistema GetById(int id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<Ecosistema> GetSelected(IEnumerable<int> ids)
        {
            return _repo.GetSelectedById(ids);
        }
    }
}

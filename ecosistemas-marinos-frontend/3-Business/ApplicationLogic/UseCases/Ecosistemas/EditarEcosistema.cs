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
    public class EditarEcosistema : IUpdate<Ecosistema>
    {
        private IRepositorioEcosistema _repo;
        public EditarEcosistema(IRepositorioEcosistema repo)
        {
            _repo = repo;
        }
        public void Update(int id, Ecosistema obj)
        {
            _repo.Update(obj, id);
        }
    }
}

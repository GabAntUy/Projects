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
    public class BorrarEcosistema : IDelete<Ecosistema>
    {
        private IRepositorioEcosistema _repo;
        public BorrarEcosistema(IRepositorioEcosistema repo)
        {
            _repo = repo;
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}

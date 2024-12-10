using ApplicationLogic.Interfaces;
using BusinessLogic.ApiDTO;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Ecosistemas
{
    public class AltaEcosistema : ICreate<EcosistemaApi>
    {
        private IRepositorioEcosistema _repo;
        public AltaEcosistema(IRepositorioEcosistema repo)
        {
            _repo = repo;
        }

        public void Create(EcosistemaApi obj)
        {
             _repo.Add(obj);
        }
    }
}

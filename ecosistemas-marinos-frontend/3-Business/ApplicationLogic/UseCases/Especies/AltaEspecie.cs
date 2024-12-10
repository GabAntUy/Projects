using ApplicationLogic.Interfaces;
using BusinessLogic.ApiDTO;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Especies
{
    public class AltaEspecie : ICreate<EspecieApi>
    {
        private IRepositorioEspecie _repo;
        public AltaEspecie(IRepositorioEspecie repo)
        {
            _repo = repo;
        }
        public void Create(EspecieApi obj)
        {
            _repo.Add(obj);
        }
    }
}

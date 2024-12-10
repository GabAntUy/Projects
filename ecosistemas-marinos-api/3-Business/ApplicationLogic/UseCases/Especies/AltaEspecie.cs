using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Especies
{
    public class AltaEspecie : ICreate<Especie>
    {
        private IRepositorioEspecie _repo;
        public AltaEspecie(IRepositorioEspecie repo)
        {
            _repo = repo;
        }
        public void Create(Especie obj)
        {
            _repo.Add(obj);
        }
    }
}

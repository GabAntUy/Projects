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
    public class EditarEspecie : IUpdate<Especie>, IAddEcosistema<Especie>
    {
        private IRepositorioEspecie _repo;
        public EditarEspecie(IRepositorioEspecie repo) 
        {
            _repo = repo;
        }

        public void AddEcosistema(Ecosistema eco, Especie esp)
        {
            _repo.AsignarEcosistema(eco, esp);
        }

        public void Update(int id, Especie obj)
        {
            _repo.Update(obj, id);
        }
    }
}

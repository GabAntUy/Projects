using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Paises
{
    public class ListarPais : IGetAll<Pais>, IGetSelected<Pais>
    {
        private IRepositorioPais _repo;
        public ListarPais(IRepositorioPais repo)
        {
            _repo = repo;
        }
        public IEnumerable<Pais> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<Pais> GetSelected(IEnumerable<int> ids)
        {
            return _repo.GetSelectedById(ids);
        }
    }
}

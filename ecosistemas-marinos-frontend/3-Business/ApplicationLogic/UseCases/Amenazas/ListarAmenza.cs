using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Amenazas
{
    public class ListarAmenza : IGetAll<Amenaza>
    {
        private IRepositorioAmenaza _repo;

        public ListarAmenza(IRepositorioAmenaza repo)
        {
            _repo = repo;
        }

        public IEnumerable<Amenaza> GetAll()
        {
            return _repo.GetAll();
        }


    }
}

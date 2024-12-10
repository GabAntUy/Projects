using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.EstadosDeConservacion
{
    public class ListarEstadoDeConservacion : IGet<EstadoDeConservacion>, IGetAll<EstadoDeConservacion>
    {
        private IRepositorioEstadoDeConservacion _repo;
        public ListarEstadoDeConservacion(IRepositorioEstadoDeConservacion repo)
        {
            _repo = repo;
        }

        public IEnumerable<EstadoDeConservacion> GetAll()
        {
            return _repo.GetAll();
        }

        public EstadoDeConservacion GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}

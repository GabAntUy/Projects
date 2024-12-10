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
    public class ListarEspecie : IGet<Especie>, IGetAll<Especie>, IGetAllByString<Especie>, 
        IGetAllByEcosistema<Especie>, IGetEspeciePorPeso<Especie>,
        IGetEspeciesPeligro<Especie>
    {
        private IRepositorioEspecie _repo;
        public ListarEspecie(IRepositorioEspecie repo)
        {
            _repo = repo;
        }

        public IEnumerable<Especie> GetAll()
        {
            return _repo.GetAll();
        }

        public Especie GetById(int id)
        {
           return _repo.GetById(id);      
        }
        public IEnumerable<Especie> GetAllByString(string str)
        {
            return _repo.GetAllByString(str);
        }
       
        public IEnumerable<Especie> GetEspeciesPeligro()
        {
            return _repo.GetEspeciesPeligro();
        }
        public IEnumerable<Especie> GetEspeciePorPeso(int pesoMin, int pesoMax)
        {
            return _repo.GetEspeciesPeso(pesoMin,pesoMax);
        }

        public IEnumerable<Especie> GetAllByEcosistema(string str)
        {
            return _repo.GetAllByEcosistema(str);
        }
    }
}

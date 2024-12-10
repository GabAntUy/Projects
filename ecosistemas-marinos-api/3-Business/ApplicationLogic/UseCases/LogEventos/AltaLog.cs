using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Logs
{
    public class AltaLog : ICreate<Log>
    {
        private IRepositorioLog _repo;
        public AltaLog(IRepositorioLog repo) 
        {
            _repo = repo;
        }
        public void Create(Log obj)
        {
            _repo.Add(obj);
        }
    }
}

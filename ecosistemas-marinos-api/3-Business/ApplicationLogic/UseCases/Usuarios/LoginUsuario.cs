using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Usuarios
{
    public class LoginUsuario : ILogin <Usuario>
    {
        private IRepositorioUsuario _repo;
        public LoginUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public Usuario Login(string alias, string contrasenia)
        {
          return  _repo.Login(alias, contrasenia);
        }
       
    }
}


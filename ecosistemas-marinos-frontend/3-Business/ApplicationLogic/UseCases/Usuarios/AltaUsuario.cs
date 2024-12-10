using ApplicationLogic.Interfaces;
using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.UseCases.Usuarios
{
    public class AltaUsuario : ICreate<UsuarioApi>
    {
        private IRepositorioUsuario _repo;
        public AltaUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public  void Create(UsuarioApi usuario)
        {
            _repo.Add(usuario);
        }
    }
}

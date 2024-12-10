using BusinessLogic.ApiDTO;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.Exceptions;
using DataAccessLogic.Exceptions.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private EcosistemasMarinosContext _context;

        public RepositorioUsuario(EcosistemasMarinosContext context)
        {
            _context = context;
        }

        public Usuario? Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new UsuarioRepositorioException("Usuario o contraseña vacíos");
            }

            try
            {
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Alias.ToLower() == username.ToLower());

                if (usuario != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, usuario.ContraseniaCifrada))
                    {
                        return usuario;
                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }


        public void Add(Usuario obj)
        {
            if (obj == null) throw new UsuarioRepositorioException("El objeto es null");

            if (GetByAlias(obj.Alias) != null) throw new UsuarioRepositorioException("Usuario ya existente");

            obj.Validar();
            obj.CifrarContraseña();
            try
            {
                obj.FechaIngreso = DateTime.Now;
                _context.Usuarios.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }
        }

        public Usuario GetByAlias(string alias)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Alias.ToLower() == alias.ToLower());
                return usuario;
            }
            catch (Exception)
            {

                throw new InfraException("Hubo un problema");
            }

        }

        public void Add(UsuarioApi obj)
        {
            throw new NotImplementedException();
        }
    }
}

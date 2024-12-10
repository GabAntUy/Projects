using BusinessLogic.Exceptions.Log;
using BusinessLogic.InterfacesDeDominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Log :IValidable, IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int IdEntidad { get; set; }
        public string TipoEntidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public void Validar()
        {
            ValidarIdentidad();
            ValidarUsername();
        }

        private void ValidarUsername()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new UserNameLogException("El atributo usuario del log no puede ser nulo");
        }
        private void ValidarIdentidad()
        {
            if (IdEntidad == 0 || IdEntidad == null)
                throw new IdEntidadLogException("El atributo usuario del log no puede ser nulo");
        }
    }

    
}

using BusinessLogic.Configuration;
using BusinessLogic.Exceptions.Ecosistema;
using BusinessLogic.Exceptions.Nombre;
using BusinessLogic.InterfacesDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities.ValueObjects.Generic
{
    public class Nombre : IValidable
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            private set { _value = value; }
        }
        public Nombre(string value) 
        {
            _value = value;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_value))
            {
                throw new NombreException("El Nombre es requerido");
            }
            if (_value.Length < Config.NombreMinimo || _value.Length > Config.NombreMaximo)
            {
                throw new NombreException($"El Nombre debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
            }
        }
    }
}

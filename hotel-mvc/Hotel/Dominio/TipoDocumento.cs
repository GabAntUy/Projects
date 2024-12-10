using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoDocumento: IValidable, IEquatable<TipoDocumento>
    {
        private static int s_ultId;
        public int Id { get; set; }
        public string Nombre { get; set; }    

        public TipoDocumento(string nombre)
        {
            Id = ++s_ultId;
            Nombre = nombre;    
        }

        public TipoDocumento() { }

        public void Validar()
        {
            if (!Utilidades.StringValido(Nombre))
            {
                throw new Exception("El nombre del tipo de documento no puede ser vacío");
            }
            if ((Nombre != "CEDULA") && (Nombre != "PASAPORTE") && (Nombre != "OTROS"))
            {
                throw new Exception("El Documento debe ser CEDULA, PASAPORTE u OTROS");
            }
        }

        public bool Equals(TipoDocumento? other)
        {
            return other != null && Id == other.Id;
        }
    }
}

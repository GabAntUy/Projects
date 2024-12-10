using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase pública Operador que hereda de la clase Usuario, en este momento sin utilidad para las funcionalidades requeridas en el obligatorio
    /// </summary>
    public class Operador : Usuario, IEquatable<Operador>
    {
        public string Nombre { get; set; }    
        public string Apellido { get; set; }
        public DateTime FechaInicioTrabajar { get; set; }

        /// <summary>
        /// Constructor de la clase Operador
        /// </summary>
        /// <param name="email"></param>
        /// <param name="contrasena"></param>
        public Operador(string email, string contrasena, string nombre, string apellido, DateTime fechaInicioTrabajar) : base(email, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaInicioTrabajar = fechaInicioTrabajar;
        }

        /// <summary>
        /// Método de validación que hereda de la clase Usuario
        /// </summary>
        public override void Validar()
        {
            base.Validar(); 
        }

        /// <summary>
        /// Devuelve el rol del usuario
        /// </summary>
        /// <returns>El rol del usuario</returns>
        public override string ObtenerRol()
        {
            return "Operador";
        }

        /// <summary>
        /// Compara si el operador actual es igual al operador proporcionado
        /// </summary>
        /// <param name="other">El operador a comparar</param>
        /// <returns>
        /// true si el operador actual es igual al operador proporcionado; de lo contrario, false
        /// </returns>
        public bool Equals(Operador? other)
        {
            return other!=null && Email == other.Email;
        }
    }
}

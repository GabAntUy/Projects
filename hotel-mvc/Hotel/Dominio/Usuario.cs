using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    /// <summary>
    /// Clase abstracta pública Usuario, las clases Huesped y Operador heredan de esta clase
    /// Implementa las interfaces IValidable y IEquatable
    /// </summary>
    public abstract class Usuario : IValidable, IEquatable<Usuario>
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="contrasena"></param>
        public Usuario(string email, string contrasena)
        {
            Email = email;
            Contrasena = contrasena;
        }

        public Usuario() { }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public virtual void Validar()
        {
            ValidarEmail();
            ValidarContrasena();
        }

        /// <summary>
        /// Valida que el Email no sea null o vacío, que contenga el carácter @ y que éste no aparezca ni al final ni al principio del Email
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarEmail()
        {
            if (!Utilidades.StringValido(Email))
            {
                throw new Exception("El email no puede ser vacío");
            }
            if (!Email.Contains('@'))
            {
                throw new Exception("El email debe contener el carácter @");
            }
            if (Email.StartsWith("@") || Email.EndsWith("@"))
            {
                throw new Exception("El carácter @ presente en el email no puede estar al comienzo ni el final del mismo ");
            }
        }

        /// <summary>
        /// Valida que la Contrasena no sea null o vacía y que tenga un mínimo de 8 caracteres
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarContrasena()
        {
            if (!Utilidades.StringValido(Contrasena))
            {
                throw new Exception("La contraseña no puede ser vacía");
            }
            if (Contrasena.Length < 8)
            {
                throw new Exception("La contraseña debe tener un mínimo de 8 caracteres");
            }
        }

        /// <summary>
        /// Muestra información de un objeto usuario 
        /// </summary>
        /// <returns>Un string con la información de un objeto usuario</returns>
        public override string ToString()
        {
            string respuesta = string.Empty;

            respuesta += $"EMail: {Email}\n";
            return respuesta;
        }

        /// <summary>
        /// Compara si dos usuarios son iguales(se usa en el Contains())
        /// </summary>
        /// <param name="other">Es el otro usuario con el que se comparará</param>
        /// <returns>Un booleano que indica si las dos usuarios son iguales o no, si el Email de los usuarios son iguales devuelve true</returns>
        public bool Equals(Usuario? other)
        {
            return other != null && Email == other.Email;
        }

        /// <summary>
        /// Devuelve el rol del usuario
        /// </summary>
        /// <returns>El rol del usuario</returns>
        public abstract string ObtenerRol();
    }
}

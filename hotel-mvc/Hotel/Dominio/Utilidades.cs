using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase pública estática Utilidades, contiene métodos útiles para realizar diversas tareas
    /// Por ser una clase estática todos sus métodos son accesibles sin necesidad de crear una objeto de la clase 
    /// </summary>
    public static class Utilidades
    {
        /// <summary>
        /// Método estático que evalúa si un valor de tipo string es null o vacío
        /// </summary>
        /// <param name="valor">Es un string evaluado por el método IsNullOrEmpty</param>
        /// <returns>Un booleano, false si es null o vacío el parámetro y true en caso contrario</returns>
        public static bool StringValido(string valor)
        {
            bool exito = false;

            if (string.IsNullOrEmpty(valor))
            {
                exito = false;
            }
            else
            {
                exito = true;
            }
            return exito;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    /// <summary>
    /// Clase pública Confirmacion, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Tercerizada
    /// Implementa la interface IValidable
    /// </summary>
    public class Confirmacion : IValidable
    {
        public Proveedor Proveedor { get; set; }

        /// <summary>
        /// Se asume que un objeto de la clase Confirmacion puede tener un proveedor pero no una fecha de confirmación, para eso se hace nulable DateTime
        /// </summary>
        public DateTime? FechaDeConfirmacion { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="proveedor"></param>
        /// <param name="fechaDeConfirmacion"></param>
        public Confirmacion(Proveedor proveedor, DateTime? fechaDeConfirmacion)
        {
            Proveedor = proveedor;
            FechaDeConfirmacion = fechaDeConfirmacion;
        }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public void Validar()
        {
            ValidarProveedor();
        }

        /// <summary>
        /// Valida que el Proveedor no sea null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarProveedor()
        {
            if (Proveedor == null)
            {
                throw new Exception("El proveedor no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que FechaDeConfirmacion no sea null
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void TieneFechaConfirmada()
        {
            if (FechaDeConfirmacion == null)
            {
                throw new Exception("La actividad no está confirmada por el proveedor");
            }
        }
    }
}

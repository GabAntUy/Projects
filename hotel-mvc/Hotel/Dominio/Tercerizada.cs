using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    /// <summary>
    /// Clase pública Tercerizada que hereda de la clase Actividad, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Agenda
    /// </summary>
    public class Tercerizada : Actividad
    {
        public Confirmacion Confirmacion { get; set; }

        /// <summary>
        /// Constructor de la clase Tercerizada, costo queda definido como parametro opcional asignandole el valor 0 por defecto
        /// </summary>
        /// <param name="nombreActividad"></param>
        /// <param name="descripcion"></param>
        /// <param name="fecha"></param>
        /// <param name="cantidadMaximaPersonas"></param>
        /// <param name="edadMinima"></param>
        /// <param name="confirmacion"></param>
        /// <param name="costo"></param>
        public Tercerizada(string nombreActividad, string descripcion, DateTime fecha, int cantidadMaximaPersonas, int edadMinima, Confirmacion confirmacion, decimal costo = 0) : base(nombreActividad, descripcion, fecha, cantidadMaximaPersonas, edadMinima, costo)
        {
            Confirmacion = confirmacion;
        }

        /// <summary>
        /// Sobreescribe Validar() y ejecuta todo los métodos de validación de la clase, además ejecuta el método Validar() de la clase base
        /// </summary>
        public override void Validar()
        {
            base.Validar();
            ValidarConfirmacion();
        }

        /// <summary>
        /// Valida que Confirmacion no sea null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarConfirmacion()
        {
            if (Confirmacion == null)
            {
                throw new Exception("La confirmación de la actividad tercerizada no puede ser null");
            }
        }

        /// <summary>
        /// Determina si un objeto confirmacion tiene fecha de confirmación, la llamará un objeto Tercerizada dentro del método AgregarActividad() para realizar la validación
        /// </summary>
        public void EstaConfirmada()
        {
            Confirmacion.TieneFechaConfirmada();
        }

        /// <summary>
        /// Sobreescribe el método de la clase base, calcula el costo final de una actividad de clase Tercerizada función del descuento del proveedor
        /// <param name="huesped"></param>
        public override decimal CalcularCostoFinal(decimal descuento)
        {
            return CostoBase - Confirmacion.Proveedor.Descuento / 100 * CostoBase;
        }

        /// <summary>
        /// Muestra información de un objeto Tercerizada, en este caso solo mostrará información de un objeto de la clase base
        /// </summary>
        /// <returns>Un string con la información de un objeto hostal</returns>
        public override string ToString()
        {
            string respuesta = base.ToString();

            return respuesta;
        }
    }
}

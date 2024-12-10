using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    /// <summary>
    /// Clase pública Hostal que hereda de la clase Actividad, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Agenda
    /// </summary>
    public class Hostal : Actividad
    {
        public string Responsable { get; set; }
        public bool AlAireLibre { get; set; }
        public string Lugar { get; set; }

        /// <summary>
        /// Constructor de la clase Hostal, costo queda definido como parametro opcional asignandole el valor 0 por defecto
        /// </summary>
        /// <param name="nombreActividad"></param>
        /// <param name="descripcion"></param>
        /// <param name="fecha"></param>
        /// <param name="cantidadMaximaPersonas"></param>
        /// <param name="edadMinima"></param>
        /// <param name="lugar"></param>
        /// <param name="responsable"></param>
        /// <param name="alAireLibre"></param>
        /// <param name="costo"></param>
        public Hostal(string nombreActividad, string descripcion, DateTime fecha, int cantidadMaximaPersonas, int edadMinima, string lugar, string responsable, bool alAireLibre, decimal costo = 0) : base(nombreActividad, descripcion, fecha, cantidadMaximaPersonas, edadMinima, costo)
        {
            Lugar = lugar;
            Responsable = responsable;
            AlAireLibre = alAireLibre;
        }

        /// <summary>
        /// Sobreescribe Validar() y ejecuta todo los métodos de validación de la clase, además ejecuta el método Validar() de la clase base
        /// </summary>
        public override void Validar()
        {
            base.Validar();
            ValidarResponsable();
            ValidarLugar();
        }

        /// <summary>
        /// Valida que Responsable no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarResponsable()
        {
            if (!Utilidades.StringValido(Responsable))
            {
                throw new Exception("El nombre del responsable no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que Lugar no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarLugar()
        {
            if (!Utilidades.StringValido(Lugar))
            {
                throw new Exception("El nombre del lugar no puede ser vacío");
            }
        }

        /// <summary>
        /// Sobreescribe el método de la clase base, calcula el costo final de una actividad de clase hostal
        /// El parámetro huesped llama a CacularDescuentoActividadHostal() lo cual devuelve el descuento porcentual que se resta al costo inicial
        /// </summary>
        /// <param name="huesped"></param>
        public override decimal CalcularCostoFinal(decimal descuento)
        {
            return CostoBase - descuento / 100 * CostoBase;
        }

        /// <summary>
        /// Muestra información deun objeto hostal, en este caso solo mostrará información de un objeto de la clase base
        /// </summary>
        /// <returns>Un string con la información de un objeto hostal</returns>
        public override string ToString()
        {
            string respuesta = base.ToString();

            return respuesta;
        }
    }
}

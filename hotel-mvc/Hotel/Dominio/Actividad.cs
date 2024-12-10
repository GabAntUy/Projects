using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase pública abstracta Actividad, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Agenda
    /// Implementa las interfaces IValidable, IComparable y IEquatable
    /// </summary>
    public abstract class Actividad : IValidable, IComparable<Actividad>, IEquatable<Actividad>
    {
        /// <summary>
        /// Variable privada estática utilizada para memorizar el  Id del último objeto instanciado.
        /// </summary>
        private static int s_ultId;
        public int Id { get; set; }
        public string NombreActividad { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadMaximaPersonas { get; set; }
        public int EdadMinima { get; set; }
        public int CuposDisponibles { get; set; }
        public decimal CostoBase { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="nombreActividad"></param>
        /// <param name="descripcion"></param>
        /// <param name="fecha"></param>
        /// <param name="cantidadMaximaPersonas"></param>
        /// <param name="edadMinima"></param>
        /// <param name="costo"></param>
        public Actividad(string nombreActividad, string descripcion, DateTime fecha, int cantidadMaximaPersonas, int edadMinima, decimal costo)
        {
            NombreActividad = nombreActividad;
            Descripcion = descripcion;
            Fecha = fecha;
            CantidadMaximaPersonas = cantidadMaximaPersonas;
            EdadMinima = edadMinima;
            CostoBase = costo;
            CuposDisponibles = cantidadMaximaPersonas; 
            Id = ++s_ultId;
        }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public virtual void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
            //ValidarFecha(); Comentado para poder precargar actividades con fecha anterior a la del día, pide por letra del Obligatorio2 poder ver actividades del pasado
            //ValidarDisponibilidadActividad(); Comentado para poder precargar actividades con cupo = 0 (pedía en checklist precarga con grupo colmado)
        }

        /// <summary>
        /// Valida que el nombre de la actividad no sea un string vacío o null y que el nombre de la actividad no tenga más de 25 caracteres
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarNombre()
        {
            if (!Utilidades.StringValido(NombreActividad))
            {
                throw new Exception("El nombre de la actividad no puede ser vacío");
            }
            if (NombreActividad.Length > 25)
            {
                throw new Exception("El nombre de la actividad no puede tener más de 25 caracteres");
            }
        }

        /// <summary>
        /// Valida que la descripción de la actividad no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarDescripcion()
        {
            if (!Utilidades.StringValido(Descripcion))
            {
                throw new Exception("La descripción de la actividad no puede ser vacía");
            }
        }

        /// <summary>
        /// Valida que la fecha de la actividad sea mayor o igual que la fecha del día
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarFecha()
        {
            if (Fecha.Date < DateTime.Today)
            {
                throw new Exception("La fecha de la actividad debe ser igual o mayor a la fecha de hoy");
            }
        }

        /// <summary>
        /// Valida si hay cupos disponibles en la actividad
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarDisponibilidadActividad()
        {
            if (CuposDisponibles <= 0)
            {
                throw new Exception("La actividad ya no tiene cupos disponibles");
            }
        }
                
        /// <summary>
        /// Método abstracto para calcular el costo final de la actividad
        /// </summary>
        /// <param name="huesped">Es el huésped que participará en la actividad</param>
        public abstract decimal CalcularCostoFinal(decimal descuento);

        /// <summary>
        /// Muestra información de un objeto actividad 
        /// </summary>
        /// <returns>Un string con la información de un objeto actividad</returns>
        public override string ToString()
        {
            string respuesta = string.Empty;

            respuesta += $"Id: {Id}\n";
            respuesta += $"Nombre: {NombreActividad}\n";
            respuesta += $"Descripción: {Descripcion}\n";
            respuesta += $"Fecha: {Fecha.ToShortDateString()}\n";
            respuesta += $"Cantidad máxima de personas: {CantidadMaximaPersonas}\n";
            respuesta += $"Edad mínima para realizar la actividad: {EdadMinima} años\n";
            respuesta += $"Costo: U$S{CostoBase}\n";
            return respuesta;
        }

        /// <summary>
        /// Compara dos actividades por NombreActividad(se usa en el Sort())
        /// </summary>
        /// <param name="other">Es la actividad con la que se comparará</param>
        /// <returns>Un int: 0, 1 o -1</returns>
        public int CompareTo(Actividad? other)
        {
            if (other == null)
            {
                return -1;
            }
            return NombreActividad.CompareTo(other.NombreActividad);
        }

        /// <summary>
        /// Compara si dos actividades son iguales(se usa en el Contains())
        /// </summary>
        /// <param name="other">Es la actividad con la que se comparará</param>
        /// <returns>Un booleano que indica si las dos actividades son iguales o no, si la Id de las actividades son iguales devuelve true</returns>
        public bool Equals(Actividad? other)
        {
            return other != null && other.Id == Id;
        }
    }
}

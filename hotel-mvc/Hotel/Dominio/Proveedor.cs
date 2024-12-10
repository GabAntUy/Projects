using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase pública Proveedor, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase de asociación Confirmacion
    /// Implementa las interfaces IEquatable, IComparable e IEquatable
    /// </summary>
    public class Proveedor : IValidable, IComparable<Proveedor>, IEquatable<Proveedor>
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public decimal Descuento { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="descuento"></param>
        public Proveedor(string nombre, string telefono, string direccion, decimal descuento = 0)
        {
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            Descuento = descuento;
        }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public void Validar()
        {
            ValidarNombre();
            ValidarTelefono();
            ValidarDireccion();
            ValidarDescuento();
        }

        /// <summary>
        /// Valida que el nombre del proveedor no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarNombre()
        {
            if (!Utilidades.StringValido(Nombre))
            {
                throw new Exception("El nombre del proveedor de la actividad tercerizada no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que el teléfono del proveedor no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarTelefono()
        {
            if (!Utilidades.StringValido(Telefono))
            {
                throw new Exception("El teléfono del proveedor de la actividad tercerizada no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que la dirección del proveedor no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarDireccion()
        {
            if (!Utilidades.StringValido(Direccion))
            {
                throw new Exception("La dirección del proveedor de la actividad tercerizada no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que el descuento que puede ofrecer el proveedor para sus actividades debe ser un valor numérico positivo, ya que se expresará como porcentaje
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarDescuento()
        {
            if (Descuento < 0)
            {
                throw new Exception("El descuento que puede ofrecer el proveedor para sus actividades no puede ser un valor numérico negativo. Debe ser del 0 al 100 (en términos porcentuales)");// el decuento va a ser un positivo decimal para expresarlo como porcentaje
            }
        }

        /// <summary>
        /// Muestra información de un objeto proveedor 
        /// </summary>
        /// <returns>Un string con la información de un objeto proveedor</returns>
        public override string ToString()
        {
            string respuesta = string.Empty;

            respuesta += $"Nombre: {Nombre}\n";
            respuesta += $"Teléfono: {Telefono}\n";
            respuesta += $"Dirección: {Direccion}\n";
            respuesta += $"Descuento fijo para actividades: {Descuento}%\n";
            return respuesta;
        }

        /// <summary>
        /// Compara dos proveedores por Nombre(se usa en el Sort())
        /// </summary>
        /// <param name="other">Es el proveedor con la que se comparará</param>
        /// <returns>Un int: 0, 1 o -1</returns>
        public int CompareTo(Proveedor? other)
        {
            if (other == null)
            {
                return 0;
            }
            return Nombre.CompareTo(other.Nombre);
        }

        /// <summary>
        /// Compara si dos proveedores son iguales(se usa en el Conptains())
        /// </summary>
        /// <param name="other">Es el proveedor con la que se comparará</param>
        /// <returns>Un booleano que indica si las dos proveedores son iguales o no, si el Nombre de los proveedores son iguales devuelve true</returns>
        public bool Equals(Proveedor? other)
        {
            return other != null && other.Nombre == Nombre;
        }
    }
}

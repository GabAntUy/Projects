using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    /// <summary>
    /// Clase pública Huesped que hereda de la clase Usuario, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Agenda
    /// Implementa la interface IEquatable
    /// </summary>
    public class Huesped : Usuario, IEquatable<Huesped>
    {
        public string Nombre { get; set; } 
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Habitacion { get; set; }
        public int Fidelizacion { get; set; } = 1;
        public Documento Documento { get; set; } = new Documento();

        /// <summary>
        /// Constructor de la clase Huesped
        /// </summary>
        /// <param name="email"></param>
        /// <param name="contrasena"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="habitacion"></param>
        /// <param name="fidelizacion"></param>
        /// <param name="documento"></param>
        public Huesped(string email, string contrasena, string nombre, string apellido, DateTime fechaNacimiento, string habitacion, int fidelizacion, Documento documento) : base(email, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Habitacion = habitacion;
            Fidelizacion = fidelizacion;
            Documento = documento;
        }

        public Huesped() { }

        /// <summary>
        /// Sobreescribe y ejecuta todo los métodos de validación de la clase, además ejecuta el método Validar() de la clase base
        /// </summary>
        public override void Validar()
        {
            base.Validar();
            ValidarNombre();
            ValidarApellido();
            ValidarFechaNacimiento();
            ValidarHabitacion();
            Documento.Validar();
            ValidarFidelizacion();
        }

        /// <summary>
        /// Valida que Nombre no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarNombre()
        {
            if (!Utilidades.StringValido(Nombre))
            {
                throw new Exception("El nombre del huésped no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que Apellido no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarApellido()
        {
            if (!Utilidades.StringValido(Apellido))
            {
                throw new Exception("El apellido del huésped no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que FechaNacimiento sea menor o igual a la fecha de hoy, y mayor o igual a la fecha de hoy menos 100 años
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarFechaNacimiento()
        {
            DateTime fechaHoy = DateTime.Today;
            DateTime fechaMinima = fechaHoy.AddYears(-100);

            if (FechaNacimiento > fechaHoy  )
            {
                throw new Exception("La fecha no es válida, debe ser menor o igual a la fecha de hoy");
            }
            if (FechaNacimiento < fechaMinima)
            {
                throw new Exception($"La fecha seleccionada no es válida, solo se admiten huéspedes de hasta 100 años de edad (la menor fecha de nacimiento permitida es {fechaMinima.ToShortDateString()})");
            }
        }

        /// <summary>
        /// Valida que Habitacion no sea un string vacío o null
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarHabitacion()
        {
            if (!Utilidades.StringValido(Habitacion))
            {
                throw new Exception("El nombre de la habitación no puede ser vacío");
            }
        }

        /// <summary>
        /// Valida que el valor de Fidelización no sea mayor a 4 ni menor a 1
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarFidelizacion()
        {
            if (Fidelizacion < 1 || Fidelizacion > 4)
            {
                throw new Exception("El valor númerico de la fidelización del huésped debe ser: 1, 2, 3 o 4");
            }
        }

        /// <summary>
        /// Calcula el descuento de la actividad del hostal según el nivel de fidelización del huésped
        /// No hacía falta declarar el método como decimal por los valores de los parámetros actuales (int), pero se optó por usar decimal para dejarla reutilizable para casos futuros donde los valores puedan ser decimales
        /// </summary>
        /// </summary>
        /// <param name="fidelizacion1"></param>
        /// <param name="fidelizacion2"></param>
        /// <param name="fidelizacion3"></param>
        /// <param name="fidelizacion4"></param>
        /// <returns>El descuento que corresponda según la fidelización del huésped</returns>
        public decimal ObtenerDescuentoActividadHostal()
        {
            decimal descuento = 0;

            switch (Fidelizacion)
            {
                case 1:
                    descuento = 0;
                    break;
                case 2:
                    descuento = 10;
                    break;
                case 3:
                    descuento = 15;
                    break;
                case 4:
                    descuento = 20;
                    break;
            }
            return descuento;
        }

        /// <summary>
        /// Calcula la edad del huésped en base a su fecha de nacimiento y la fecha actual
        /// </summary>
        /// <returns>La edad del huésped.</return
        public int CalcularEdadHuesped()
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - FechaNacimiento.Year;

            //si la fecha de hoy (DateTime.Now) es menor que la fecha de cumpleaños de este año,
            //quiere decir que todavia no paso el cumpleaños este año y se le resta un año
            if (fechaActual < FechaNacimiento.AddYears(edad))
            {
                edad--;
            }
            return edad;
        }

        /// <summary>
        /// Muestra información de un objeto huésped, se llama al ToString de la clase base por lo que se mostrá lo que hereda del ToString de Usuario
        /// </summary>
        /// <returns>Un string con la información de un objeto huésped</returns>
        public override string ToString()
        {
            string respuesta = base.ToString();

            respuesta += $"Nombre: {Nombre}\n";
            respuesta += $"Apellido: {Apellido}\n";
            respuesta += $"Fecha de nacimiento: {FechaNacimiento.ToShortDateString()}\n";
            respuesta += $"Habitación: {Habitacion}\n";
            respuesta += $"Fidelización: {Fidelizacion}\n";
            respuesta += $"Documento: {Documento.TipoDocumento.Nombre}-{Documento.Numero}\n";
            return respuesta;
        }

        /// <summary>
        /// Compara si dos huéspedes son iguales(se usa en el Contains())
        /// </summary>
        /// <param name="other">Es el huésped con la que se comparará</param>
        /// <returns>Un booleano que indica si los dos huéspedes son iguales o no, si Tipo e Id  de los documentos son iguales devuelve true </returns>
        public bool Equals(Huesped? other)
        {
            return other != null && other.Documento.TipoDocumento.Id == Documento.TipoDocumento.Id && other.Documento.Numero == Documento.Numero;
        }

        /// <summary>
        /// Devuelve el rol del usuario
        /// </summary>
        /// <returns>El rol del usuario</returns>
        public override string ObtenerRol()
        {
            return "Huesped";
        }
    }
}



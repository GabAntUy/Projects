using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase pública Agenda, al instanciarse un objeto de esta clase se define una agenda con un objeto actividad y un objeto huésped
    /// Implementa las interfaces IValidable y IEquatable
    /// </summary>
    public class Agenda : IValidable, IEquatable<Agenda>, IComparable<Agenda>
    {
        public DateTime FechaCreacion { get; set; }
        public EstadoAgenda Estado { get; set; }
        public Huesped Huesped { get; set; }
        public Actividad Actividad { get; set; }

        private static int s_ultId;
        public int Id { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="huesped"></param>
        /// <param name="actividad"></param>
        /// <exception cref="Exception"></exception>
        public Agenda(Huesped huesped, Actividad actividad)
        {
            FechaCreacion = DateTime.Today;
            Huesped = huesped;
            Actividad = actividad;
            Id = ++s_ultId;
            AsignarEstado();
        }

        public Agenda() { }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public void Validar()
        {
            ValidarHuesped();
            ValidarActividad();
            ValidarEdadMinimaHuesped();
            Actividad.ValidarDisponibilidadActividad();
            ValidarConfirmacionActividadTercerizada();
        }

        /// <summary>
        /// Valida que el huesped de la agenda no sea null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarHuesped()
        {
            if (Huesped == null)
            {
                throw new Exception("El huésped no puede ser null");
            }
        }

        /// <summary>
        /// Valida que la actividad de la agenda no sea null
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarActividad()
        {
            if (Actividad == null)
            {
                throw new Exception("La actividad no puede ser null");
            }
        }

        /// <summary>
        /// Valida que el huésped tenga la edad mínima requerida para la actividad
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ValidarEdadMinimaHuesped()
        {
            if (Huesped.CalcularEdadHuesped() < Actividad.EdadMinima)
            {
                throw new Exception("El huésped no cuenta con la edad mínima para poder agendarse en la actividad");
            }
        }

        /// <summary>
        /// Valida la confirmación de una actividad tercerizada 
        /// Si la actividad es Tercerizada, se llama al método EstaConfirmada() de la clase Tercerizada
        /// </summary>
        private void ValidarConfirmacionActividadTercerizada()
        {
            if (Actividad is Tercerizada unaT)
            {
                unaT.EstaConfirmada();
            }
        }

        public decimal CostoFinalActividad()
        {
            return Actividad.CalcularCostoFinal(Huesped.ObtenerDescuentoActividadHostal());
        }

        private void AsignarEstado()
        {
            Estado = CostoFinalActividad() == 0 ? EstadoAgenda.CONFIRMADA : EstadoAgenda.PENDIENTE_PAGO;
        }

        public void SetEstado(EstadoAgenda estado)
        {
            Estado = estado;
        }

        /// <summary>
        /// Muestra el costo de la actividad 
        /// </summary>
        /// <returns>El costo de la actividad como un string</returns>
        public string MostrarCosto()
        {
            if (CostoFinalActividad() == 0)
            {
                return "Actividad gratuita";
            }
            return $"U$S{CostoFinalActividad()}";
        }

        /// <summary>
        /// Muestra información de un objeto agenda 
        /// </summary>
        /// <returns>Un string con la información de un objeto agenda</returns>
        public override string ToString()
        {
            string respuesta = string.Empty;

            respuesta += $"Nombre y apellido del huésped: {Huesped.Nombre} {Huesped.Apellido}\n";
            respuesta += $"Nombre de la actividad: {Actividad.NombreActividad}\n";
            respuesta += $"Fecha de la actividad: {Actividad.Fecha.ToShortDateString()}\n";
            if (Actividad is Hostal unaA)
            {
                respuesta += $"Lugar: {unaA.Lugar}\n";
            }
            respuesta += $"Costo: {MostrarCosto()}\n";
            respuesta += $"Estado: {Estado}\n";
            if (Actividad is Tercerizada unaT)
            {
                respuesta += $"Nombre del proveedor: {unaT.Confirmacion.Proveedor.Nombre}\n";
            }
            return respuesta;
        }

        /// <summary>
        /// Compara si dos agendas son iguales(se usa en el Contains()) 
        /// </summary>
        /// <param name="other">Es la agenda con la que se comparará</param>
        /// <returns>Un booleano que indica si las dos actividades son iguales o no</returns>
        public bool Equals(Agenda? other)
        {
            return other != null && Huesped.Email == other.Huesped.Email && Actividad.Id == other.Actividad.Id;
        }

        public int CompareTo(Agenda? other)
        {
            if (other == null) return -1;
            int orden = Actividad.Fecha.CompareTo(other.Actividad.Fecha);
            if (orden == 0)
            {
                orden = Actividad.NombreActividad.CompareTo(other.Actividad.NombreActividad);
            }
            return orden;
        }
    }
}

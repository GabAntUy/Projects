using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    /// <summary>
    /// Clase pública Documento, se podrá instanciar un objeto de esta clase y pasarlo como parámetro al constructor de la clase Huesped
    /// Implementa las interfaces IValidable y IEquatable
    /// </summary>
    public class Documento : IValidable, IEquatable<Documento>
    {
        public TipoDocumento TipoDocumento { get; set; } = new TipoDocumento();
        public string Numero { get; set; }

        /// <summary>
        /// Constructor de la clase 
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="numero"></param>
        public Documento(TipoDocumento tipoDocumento, string numero)
        {
            TipoDocumento = tipoDocumento;
            Numero = numero;
        }

        public Documento() { }

        /// <summary>
        /// Ejecuta todo los métodos de validación de la clase
        /// </summary>
        public void Validar()
        {
            ValidarTipoDocumento();
            ValidarNumero();
            if (TipoDocumento.Nombre == "CEDULA")
            {
                ValidarCedula();
            }
        }

        /// <summary>
        /// Valida que Tipo no sea un string vacío o null y que sea CEDULA o PASAPORTE u OTROS
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarTipoDocumento()
        {
            if (TipoDocumento == null)
            {
                throw new Exception("El tipo de Documento no debe ser vacío");
            }
        }

        /// <summary>
        /// Valida que Id no sea un string vacío o null 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarNumero()
        {
            if (!Utilidades.StringValido(Numero))
            {
                throw new Exception("El número de documento no debe estar vacío");
            }
        }

        /// <summary>
        /// Valida la cédula
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ValidarCedula()
        {
            int j = 0;
            if (!int.TryParse(Numero, out j ))
            {
                throw new Exception("La Cedula ingresada no es válida, solo debe contener números");
            }
            int multiplicacion = 0;
            string verificador = "2987634";
            int numMayorTerminadoEnCero = 0;
            int digitoVerificador;

            // Si tiene menos de 7 dígitos agrego el resto
            while (Numero.Length < 7)
            {
                Numero = "0" + Numero;
            }

            // Los primeros 7 digitos de la cédula
            for (int i = 0; i < verificador.Length; i++)
            {
                multiplicacion += int.Parse(Numero[i].ToString()) * int.Parse(verificador[i].ToString());
            }

            numMayorTerminadoEnCero = multiplicacion;

            while (numMayorTerminadoEnCero % 10 != 0)
            {
                numMayorTerminadoEnCero++;
            }

            digitoVerificador = numMayorTerminadoEnCero - multiplicacion;
            int ultimoDigitoID = int.Parse(Numero[Numero.Length - 1].ToString());

            // Compruebo el dígito verificador calculado con el colocado por la persona
            if (digitoVerificador != ultimoDigitoID)
            {
                throw new Exception("La Cedula ingresada no es válida");
            }
        }

        /// <summary>
        /// Muestra información de un objeto documento
        /// </summary>
        /// <returns>Un string con la información de un objeto documento</returns>
        public override string ToString()
        {
            string respuesta = string.Empty;

            respuesta += $"Tipo Documento: {TipoDocumento.Nombre}\n";
            respuesta += $"Documento: {Numero}\n";
            return respuesta;
        }

        /// <summary>
        /// Compara si dos documentos son iguales(se usa en el Contains())
        /// </summary>
        /// <param name="other">Es el documento con la que se comparará</param>
        /// <returns>Un booleano que indica si los dos documentos son iguales o no, si el Tipo de los documentos son iguales devuelve true </returns>
        public bool Equals(Documento? other)
        {
            return other != null && TipoDocumento.Id == other.TipoDocumento.Id && Numero == other.Numero;
        }
    }
}

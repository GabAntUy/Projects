using Dominio;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppConsola
{
    internal class Program
    {
        
        /// <summary>
        /// Instanciación de objeto sistema utilizando Singleton
        /// </summary>
        static private Sistema _sistema = Sistema.Instancia;

        static void Main(string[] args)
        {
            try
            {
                //_sistema.Precargar();
                Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Muestra el menú al usuario
        /// </summary>
        static private void Menu()
        {
            //El tipo de valor de opcion es decimal dado que se modificó el tipo de valor de PedirNumero() de int a decimal 
            //Se hizo para poder reutilizar PedirNumero() en otra parte del código donde se pide un costo y es necesario poder ingresar un valor tipo decimal 
            decimal opcion;

            do
            {
                MostrarTexto("\nIngrese: \n\n" +
                    "1-Listar todas las actividades\n" +
                    "2-Listar todos los provedores\n" +
                    "3-Listar actividades filtradas por rango de fechas y costo\n" +
                    "4-Establecer valor de promoción para actividades de un proveedor\n" +
                    "5-Alta de huésped\n" +
                    "0-Salir\n");
                opcion = PedirNumero();
                switch (opcion)
                {
                    case 1:
                        MostrarListaActividades();
                        break;
                    case 2:
                        MostrarListaOrdenadaDeProveedores();
                        break;
                    case 3:
                        MostrarListaOrdenadaDeActividades();
                        break;
                    case 4:
                        AsignarDescuentoProveedor();
                        break;
                    case 5:
                        RegistrarHuesped();
                        break;
                    default:
                        break;
                }
            } while (opcion != 0);
        }

        /// <summary>
        /// Método para registrar un nuevo huésped 
        /// Este método guía al usuario para ingresar los datos necesarios para registrar un nuevo huésped
        /// </summary>
        private static void RegistrarHuesped()
        {
            MostrarTitulo("Alta de un nuevo huésped");
            MostrarTexto("Deberá ingresar los datos solicitados para registrar un nuevo huésped");
            string email = string.Empty;
            string contrasena = string.Empty;
            string nombre = string.Empty;
            string apellido = string.Empty;
            DateTime fechaNacimiento = DateTime.Now;
            string habitacion = string.Empty;
            int fidelizacion = 0;
            Documento documento = new Documento("CEDULA", "12345678");
            Huesped tmpHuesped = new Huesped(email, contrasena, nombre, apellido, fechaNacimiento, habitacion, fidelizacion, documento);

     
            AsignarNuevoEmailUsuario(tmpHuesped);
            AsignarNuevaContrasenaUsuario(tmpHuesped);
            AsignarNuevoNombreHuesped(tmpHuesped);
            AsignarNuevoApellidoHuesped(tmpHuesped);
            AsignarFechaNacimientoHuesped(tmpHuesped);
            AsignarHabitacionAHuesped(tmpHuesped);
            AsignarFidelizacionAHuesped(tmpHuesped);
            AsignarTipoDocumentoAHuesped(tmpHuesped);
            AsignarNumeroDocumentoAHuesped(tmpHuesped);
            if (tmpHuesped != null)
            {
                MostrarResultadoExitoso("El huésped ha sido registrado exitosamente");
                Sistema.Instancia.AgregarHuesped(tmpHuesped);
            }
            Console.WriteLine(_sistema.Usuarios[_sistema.Usuarios.Count-1]);
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese un nuevo email para el alta de un huésped y realiza validaciones sobre el email ingresado por el usuario
        /// </summary>
        /// <param name="user"></param>
        static public void AsignarNuevoEmailUsuario(Usuario user)
        {
            Console.WriteLine("\nIngrese el email del nuevo huésped:");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                user.Email = respuestaUsuario;
                user.ValidarEmail();
                Sistema.Instancia.ValidarUnicidadEmail(user);
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarNuevoEmailUsuario(user);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese una contraseña para el alta de un huésped y realiza validaciones sobre la contraseña ingresada por el usuario
        /// </summary>
        /// <param name="user"></param>
        static public void AsignarNuevaContrasenaUsuario(Usuario user)
        {
            Console.WriteLine("\nAsigne una contraseña al huésped:");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                user.Contrasena = respuestaUsuario;
                user.ValidarContrasena();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarNuevaContrasenaUsuario(user);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese un nombre para el alta de un huésped y realiza validaciones sobre el nombre ingresado por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarNuevoNombreHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese el nombre del huésped:");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                huesped.Nombre = respuestaUsuario;
                huesped.ValidarNombre();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarNuevoNombreHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese un apellido para el alta de un huésped y realiza validaciones sobre el apellido ingresado por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarNuevoApellidoHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese el apellido del huésped:");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                huesped.Apellido = respuestaUsuario;
                huesped.ValidarApellido();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarNuevoApellidoHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese una fecha de nacimiento para el alta de un huésped y realiza validaciones sobre la fecha de nacimiento ingresada por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarFechaNacimientoHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese la fecha de nacimiento (dd/mm/aaaa) del huésped:");
            DateTime respuestaUsuario = DateTime.Now;
            try
            {
                respuestaUsuario = PedirFecha();
                huesped.FechaNacimiento = respuestaUsuario;
                huesped.ValidarFechaNacimiento();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarFechaNacimientoHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese una habitación para el alta de un huésped y realiza validaciones sobre la habitación ingresada por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarHabitacionAHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese la habitación del huesped:");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                huesped.Habitacion = respuestaUsuario;
                huesped.ValidarHabitacion();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarHabitacionAHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese una fidelización para el alta de un huésped y realiza validaciones sobre la fidelización ingresada por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarFidelizacionAHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese la fidelización del huésped:");
            int respuestaUsuario = -1;
            try
            {
                respuestaUsuario = (int)PedirNumero();
                huesped.Fidelizacion = respuestaUsuario;
                huesped.ValidarFidelizacion();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarFidelizacionAHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese un tipo de documento para el alta de un huésped y realiza validaciones sobre el tipo de documento ingresado por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarTipoDocumentoAHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese el tipo de documento del huésped (CEDULA, PASAPORTE, OTROS):");
            string respuestaUsuario = string.Empty;
            try
            {
                respuestaUsuario = PedirTexto();
                huesped.Documento.Tipo = respuestaUsuario.ToUpper();
                huesped.Documento.ValidarTipoDocumento();
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarTipoDocumentoAHuesped(huesped);
            }
        }

        /// <summary>
        /// Este método solicita al usuario que ingrese un número de documento para el alta de un huésped y realiza validaciones sobre el número de documento ingresado por el usuario
        /// </summary>
        /// <param name="huesped"></param>
        static public void AsignarNumeroDocumentoAHuesped(Huesped huesped)
        {
            Console.WriteLine("\nIngrese el número de documento del huésped:");
            string respuestaUsuario = string.Empty;
            try
            {
                if (huesped.Documento.Tipo.ToUpper() == "CEDULA")
                {
                    respuestaUsuario = PedirNumero().ToString();
                }
                else
                {
                    respuestaUsuario = PedirTexto();
                }
                huesped.Documento.Numero = respuestaUsuario;
                huesped.Documento.ValidarNumero();

                if (huesped.Documento.Tipo == "CEDULA" )
                {
                    huesped.Documento.ValidarCedula();
                }
                Sistema.Instancia.ValidarUnicidadDocumento(huesped);
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
                AsignarNumeroDocumentoAHuesped(huesped);
            }
        }

        /// <summary>
        /// Los siguientes métodos son útiles para mostrar mensajes y para pedir datos a través de la consola
        /// </summary>
        private static void MostrarResultadoError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string final = EncuadrarTexto(error);
            Console.WriteLine(final);
            Console.ResetColor();
        }

        private static void MostrarResultadoExitoso(string exito)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string final = EncuadrarTexto(exito);
            Console.WriteLine(final);
            Console.ResetColor();
        }

        private static string EncuadrarTexto(string texto)
        {
            string textoARetornar = string.Empty;
            string textoInicio = "| ";
            string textoFin = " |";

            textoARetornar += Rayita(texto + textoInicio + textoFin);
            textoARetornar += "\n" + textoInicio + texto + textoFin + "\n";
            textoARetornar += Rayita(texto + textoInicio + textoFin);
            return textoARetornar;
        }
       
        private static string Rayita(string texto, string caracter = "-")
        {
            string linea = string.Empty;

            for (int i = 0; i < texto.Length; i++)
            {
                linea += caracter;
            }
            return linea;
        }

        private static void MostrarTitulo(string texto)
        {
            Console.WriteLine(Rayita(texto));
            Console.WriteLine(texto);
            Console.WriteLine(Rayita(texto) + "\n");
        }

        private static void MostrarTexto(string texto)
        {
            Console.WriteLine(texto);
        }

        private static string PedirTexto()
        {
            string texto = Console.ReadLine();
            return texto;
        }

        static private decimal PedirNumero()
        {
            int numero = 0;
            bool salir = false;

            do
            {
                try
                {
                    salir = true;
                    numero = int.Parse(PedirTexto());
                }
                catch (Exception)
                {
                    salir = false;
                    Console.WriteLine("Solo debe ingresar números.");
                }
            } while (!salir);
            return numero;
        }

        static private DateTime PedirFecha()
        {
            DateTime date = DateTime.Now;
            bool salir = false;

            do
            {
                try
                {
                    salir = true;
                    date = DateTime.Parse(PedirTexto());
                }
                catch (Exception)
                {
                    salir = false;
                    Console.WriteLine("Solo debe ingresar fechas (dd/mm/aaaa).");
                }
            } while (!salir);
            return date;
        }

        /// <summary>
        /// Muestra un listado de todas las actividades disponibles en el sistema
        /// </summary>
        static private void MostrarListaActividades()
        {
            MostrarTitulo("Listado de todas las actividades");
            foreach (Actividad item in Sistema.Instancia.Actividades)
            {
                MostrarTexto($"{item}");
            }
        }

        /// <summary>
        /// Método para asignar un porcentaje de descuento a las actividades de un proveedor en específico(por nombre)
        /// </summary>
        static private void AsignarDescuentoProveedor()
        {
            MostrarTitulo("Establecer valor de promoción para actividades de un proveedor");
            MostrarTexto("Deberá ingresar el nombre de un proveedor y el pocentaje de descuento para sus actividades");
            try
            {
                string nombre = PedirNombreProveedor();
                if (nombre != "-1")
                {
                    decimal descuento = PedirDescuentoProveedor();

                    Sistema.Instancia.ModificarDescuentoProveedor(nombre, descuento);
                    MostrarResultadoExitoso($"Se ha asignado un descuento de {descuento}% a las actividades del proveedor '{nombre}'.");
                    MostrarTexto($"{_sistema.ObtenerProveedorPorNombre(nombre)}");
                }
            }
            catch (Exception e)
            {
                MostrarResultadoError(e.Message);
            }
        }

        /// <summary>
        /// Método para solicitar al usuario el porcentaje de descuento que se desea asignar a las actividades del proveedor
        /// </summary>
        /// <returns></returns>
        static private decimal PedirDescuentoProveedor()
        {
            decimal descuento = 0;
            bool salir = false;

            do
            {
                try
                {
                    do
                    {
                        salir = true;

                        MostrarTexto("\nIngrese el descuento porcentual para las actividades del proveedor (-1 -Salir):");
                        descuento = decimal.Parse(PedirTexto());
                        if (descuento != -1)
                        {
                            if (descuento < 0 || descuento > 100)
                            {
                                MostrarResultadoError("El descuento porcentual no debe ser menor a 0 ni mayor a 100, vuelva a intentarlo.");
                                salir = false;
                            }
                        }
                        
                    } while (!salir);
                }
                catch (Exception)
                {
                    MostrarResultadoError("Solo debe ingresar números.");
                    salir = false;
                }
            } while (!salir);
            return descuento;
        }

        /// <summary>
        /// Solicita al usuario que ingrese el nombre de un proveedor y valida que el nombre ingresado no esté vacío y que exista un proveedor registrado con ese nombre
        /// </summary>
        /// <returns></returns>
        static private string PedirNombreProveedor()
        {
            string nombre = string.Empty;
            bool salir = false;

            do
            {
                salir = true;
                MostrarTexto("\nIngrese el nombre del proveedor (-1 -Salir):");
                nombre = PedirTexto();
                if (nombre != "-1")
                {
                    if (!Utilidades.StringValido(nombre))
                    {
                        MostrarResultadoError("Ingresó un nombre vacío, vuelva a intentarlo.");
                        salir = false;
                    }
                    else if (Sistema.Instancia.ObtenerProveedorPorNombre(nombre) == null)
                    {
                        MostrarResultadoError($"No se encontró el proveedor con el nombre '{nombre}', vuelva a intentarlo.");
                        salir = false;
                    }
                }
            } while (!salir);
            return nombre;
        }

        /// <summary>
        /// Muestra un listado de todos los proveedores ordenados alfabéticamente
        /// </summary>
        private static void MostrarListaOrdenadaDeProveedores()
        {
            MostrarTitulo("Listado de todos los proveedores (ordenado alfabéticamente)");
            foreach (Proveedor item in Sistema.Instancia.ListadoProveedoresPorOrdenAlfabetico())
            {
                MostrarTexto($"{item}");
            }
        }

        /// <summary>
        /// Muestra un listado de actividades filtrado por rango de fecha y costo, ordenado descendentemente por costo
        /// </summary>
        private static void MostrarListaOrdenadaDeActividades()
        {
            MostrarTitulo("Listado de actividades filtrado por rango de fecha y costo (ordenado descendentemente por costo)");
            MostrarTexto("Ingrese un costo en U$S (se listarán las actividades con un costo mayor al ingresado):");
            decimal costo = PedirNumero();
            MostrarTexto("Debe ingresar un rango de fechas (se listarán las actividades dentro del rango ingresado)");
            MostrarTexto("\nIngrese la fecha inical (dd/mm/aaaa):");
            DateTime fechaInicial = PedirFecha();
            MostrarTexto("\nIngrese la fecha final (dd/mm/aaaa):");
            DateTime fechaFinal = PedirFecha();
            MostrarTexto("\n");
            foreach (Actividad item in Sistema.Instancia.ListadoActividadesPorOrdenCostoDescendente(fechaInicial, fechaFinal, costo))
            {
                MostrarTexto($"{item}");
            }
        }
    }
}
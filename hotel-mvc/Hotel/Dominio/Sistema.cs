using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Agenda> _agendas = new List<Agenda>();
        private List<Actividad> _actividades = new List<Actividad>();
        private List<Proveedor> _proveedores = new List<Proveedor>();
        private List<TipoDocumento> _tipoDocumentos = new List<TipoDocumento>();

        /// <summary>
        /// Patrón de diseño singleton
        /// </summary>
        private static Sistema s_instancia = null;
        public static Sistema Instancia
        {
            get
            {
                if (s_instancia == null)
                {
                    s_instancia = new Sistema();
                }
                return s_instancia;
            }
        }
        private Sistema()
        {
            Precargar();
        }

        /// <summary>
        /// El set no se usa para forzar el uso de los métodos que se diseñaron para agregar objetos a las listas
        /// </summary>
        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
        }
        public List<Agenda> Agendas
        {
            get { return _agendas; }
        }
        public List<Actividad> Actividades
        {
            get { return _actividades; }
        }
        public List<Proveedor> Proveedores
        {
            get { return _proveedores; }
        }
        public List<TipoDocumento> TipoDocumentos
        {
            get { return _tipoDocumentos; }
        }

        /// <summary>
        /// Método que ejecuta de forma ordenada todo los métodos de precarga 
        /// </summary>
        public void Precargar()
        {
            PrecargarDatosTipoDeDocumentos();
            PrecargarDatosHuespedes();
            PrecargarDatosOperadores();
            PrecargarDatosProveedores();
            PrecargarDatosActividadesHostal();
            PrecargarDatosActividadesTercerizadas();
            PrecargarDatosAgendas();
        }

        /// <summary>
        /// Método donde se instancian y se agregan los objetos de la clase TipoDocumento
        /// </summary>
        public void PrecargarDatosTipoDeDocumentos()
        {
            TipoDocumento unT1 = new TipoDocumento("CEDULA");
            AgregarTipoDocumento(unT1);
            TipoDocumento unT2 = new TipoDocumento("PASAPORTE");
            AgregarTipoDocumento(unT2);
            TipoDocumento unT3 = new TipoDocumento("OTROS");
            AgregarTipoDocumento(unT3);
        }

        /// <summary>
        /// Método donde se instancian y se agregan los objetos de la clase Operador
        /// </summary>
        public void PrecargarDatosOperadores()
        {
            Operador unO1 = new Operador("operador1@operador", "123456789", "Federico", "Suarez", DateTime.Today.AddYears(-2));
            AgregarOperador(unO1);
            Operador unO2 = new Operador("operador2@operador", "123456789", "Juan", "López", DateTime.Today.AddYears(-3));
            AgregarOperador(unO2);
        }

        /// <summary>
        /// Método donde se instancian y se agregan lo objetos de la clase Huespede, se instancian también los documentos que se pasan como párametro al constructor de huesped 
        /// </summary>
        public void PrecargarDatosHuespedes()
        {
            Documento unD1 = new Documento(_tipoDocumentos[0], "48736392");
            Huesped unH1 = new Huesped("juan@hotmail.com", "123456789", "Juan", "González", DateTime.Today.AddYears(-25), "11", 1, AgregarDocumentoAHuesped(unD1));
            AgregarHuesped(unH1);
            Documento unD2 = new Documento(_tipoDocumentos[0], "41272084");
            Huesped unH2 = new Huesped("cecilia@hotmail.com", "123456789", "Cecilia", "López", DateTime.Today.AddYears(-20), "11", 1, AgregarDocumentoAHuesped(unD2));
            AgregarHuesped(unH2);
            Documento unD3 = new Documento(_tipoDocumentos[1], "12345");
            Huesped unH3 = new Huesped("esteban@hotmail.com", "123456789", "Esteban", "García ", DateTime.Today.AddYears(-9), "11", 1, AgregarDocumentoAHuesped(unD3));
            AgregarHuesped(unH3);


            // Prueba de agregar un huésped con el mismo número de cédula que el de otro ya agregado
            // Mensaje de error: "Ya hay un huésped registrado con el mismo documento CEDULA 41272084"
            //Documento unD3 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH3 = new Huesped("analia@hotmail.com", "87654321", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH3);

            // Prueba validación de unicidad del Email (Usuario, Huesped y Operador)
            // Mensaje de error: "Ya hay un huésped registrado con el mismo email juan@hotmail.com"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH4 = new Huesped("juan@hotmail.com", "87654321", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH4);

            // Prueba de valores que puede tomar fidelización de huésped
            // Mensaje de error: "El valor númerico de la fidelización del huésped debe ser: 1, 2, 3 o 4"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "18647133");
            //Huesped unH5 = new Huesped("nana@hotmail.com", "12345678", "Juan", "González", new DateTime(2000, 01, 01), "11", 7, AgregarDocumentoAHuesped(unD4));
            //AgregarHuesped(unH5);

            // Prueba de presencia del carácter "@" en email
            // Mensaje de error: "El email debe contener el carácter @"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH6 = new Huesped("juanhotmail.com", "87654321", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH6);

            // Prueba de que el carácter "@" no esté al final del email
            // Mensaje de error: "El carácter @ presente en el email no puede estar al comienzo ni el final del mismo"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH7 = new Huesped("juanhotmail.com@", "87654321", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH7);

            // Prueba de que el carácter "@" no esté al final del email
            // Mensaje de error: "El carácter @ presente en el email no puede estar al comienzo ni el final del mismo"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH8 = new Huesped("@juanhotmail.com", "87654321", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH8);

            // Prueba de cantidad mínima de caracteres en contraseña
            // Mensaje de error: "La contraseña debe tener un mínimo de 8 caracteres"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH8 = new Huesped("juan@hotmail.com", "4", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH8);

            // Prueba de nombre vacío
            // Mensaje de error: "El nombre del huésped no puede ser vacío"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH9 = new Huesped("juan@hotmail.com", "12345678", "Cecilia", "López", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH9);

            // Prueba de apellido vacío
            // Mensaje de error: "El apellido del huésped no puede ser vacío"
            //Documento unD4 = new Documento(_tipoDocumentos[0], "41272084");
            //Huesped unH10 = new Huesped("juan@hotmail.com", "12345678", "Cecilia", "", new DateTime(2000, 10, 02), "11", 1, AgregarDocumentoAHuesped(unD2));
            //AgregarHuesped(unH10);

            // Prueba de validación del número de cédula
            // Mensaje de error: "La Cedula ingresada no es válida"
            //Documento unD3 = new Documento(_tipoDocumentos[0], "12343456635");
            //unD3.Validar();
        }

        /// <summary>
        /// Método donde se instancian y se agregan los proveedores 
        /// </summary>
        private void PrecargarDatosProveedores()
        {
            Proveedor unP1 = new Proveedor("DreamWorks S.R.L.", "23048549", "Suarez 3380 Apto 304", 10);
            AgregarProveedor(unP1);
            Proveedor unP2 = new Proveedor("Estela Umpierrez S.A.", "33459678", "Lima 2456", 7);
            AgregarProveedor(unP2); ;
            Proveedor unP3 = new Proveedor("TravelFun", "29152020", "Misiones 1140", 9);
            AgregarProveedor(unP3);
            Proveedor unP4 = new Proveedor("Rekreation S.A.", "29162019", "Bacacay 1211", 11);
            AgregarProveedor(unP4);
            Proveedor unP5 = new Proveedor("Alonso & Umpierrez", "24051920", "18 de Julio 1956 Apto 4", 10);
            AgregarProveedor(unP5);
            Proveedor unP6 = new Proveedor("Electric Blue", "26018945", "Cooper 678", 5);
            AgregarProveedor(unP6);
            Proveedor unP7 = new Proveedor("Lúdica S.A.", "26142967", "Dublin 560", 4);
            AgregarProveedor(unP7);
            Proveedor unP8 = new Proveedor("Gimenez S.R.L.", "29001010", "Andes 1190", 7);
            AgregarProveedor(unP8);
            Proveedor unP9 = new Proveedor("Crash", "22041120", "Agraciada 2512 Apto. 1", 8);
            AgregarProveedor(unP9);
            Proveedor unP10 = new Proveedor("Norberto Molina", "22001189", "Paraguay 2100", 9);
            AgregarProveedor(unP10);

            // Prueba de nombre vacío
            // Mensaje de error: "El nombre del proveedor de la actividad tercerizada no puede ser vacío"
            //Proveedor unP11 = new Proveedor("", "22001189", "Paraguay 2100", 9);
            //AgregarProveedor(unP11);

            // Prueba de teléfono vacío
            // Mensaje de error: "El teléfono del proveedor de la actividad tercerizada no puede ser vacío"
            //Proveedor unP12 = new Proveedor("nombre", "", "Paraguay 2100", 9);
            //AgregarProveedor(unP12);

            // Prueba de dirección vacía
            // Mensaje de error: "La dirección del proveedor de la actividad tercerizada no puede ser vacío"
            //Proveedor unP13 = new Proveedor("nombre", "22001189", "", 9);
            //AgregarProveedor(unP13);

            // Prueba de valor de descuento que puede ofrecer el proveedor
            // Mensaje de error: "El descuento que puede ofrecer el proveedor para sus actividades no puede ser un valor numérico negativo. Debe ser del 0 al 100 (en términos porcentuales)"
            //Proveedor unP14 = new Proveedor("nombre", "22001189", "Paraguay 2100", -20);
            //AgregarProveedor(unP14);

            // Prueba de agregar un proveedor con mismo nombre (unicidad de nombre)
            // Mensaje de error: "El proveedor con nombre Norberto Molina ya esta registrado."
            //Proveedor unP15 = new Proveedor("Norberto Molina", "22001189", "Paraguay 2100", 9);
            //AgregarProveedor(unP15);
        }

        /// <summary>
        /// Método donde se instancian y se agregan las actividades de hostal
        /// </summary>
        private void PrecargarDatosActividadesHostal()
        {
            Hostal unaH1 = new Hostal("Piscina", "Podrá hacer uso de la piscina climatizada con supervisión de guardavidas", DateTime.Today.AddDays(2), 20, 15, "Piscina interior", "Luis Gutiérrez", false);
            AgregarActividad(unaH1);
            Hostal unaH2 = new Hostal("Gimnasia", "Podrá ejercitarse en las instalaciones deportivas asistido por un profesor", DateTime.Today.AddDays(1), 10, 16, "Gimnasio", "Pedro Fernandez", false);
            AgregarActividad(unaH2);
            Hostal unaH3 = new Hostal("Clases de baile", "Se impartirán clases de salsa nivel principiante", DateTime.Today.AddDays(2), 25, 10, "Sala multiusos", "Romina López", false);
            AgregarActividad(unaH3);
            Hostal unaH4 = new Hostal("Sauna", "El huésped podrá acceder al sauna y recibir una sesión de masajes", DateTime.Today.AddDays(2), 5, 18, "Sauna", "Susana Sanchez", false, 15);
            AgregarActividad(unaH4);
            Hostal unaH5 = new Hostal("Bingo", "Jugará al bingo junto a otros huéspedes", DateTime.Today, 50, 15, "Salón de juegos", "Federico Silva", false, 5);
            AgregarActividad(unaH5);
            Hostal unaH6 = new Hostal("Cata de vinos", "Se brindará al huésped una experiencia de cata de vinos locales", DateTime.Today, 10, 18, "Jardín interior", "Richard Sosa", true);
            AgregarActividad(unaH6);
            Hostal unaH7 = new Hostal("Clases de yoga", "Se brindará una sesión guiada por un instructor", DateTime.Today.AddDays(-1), 15, 10, "Sala de yoga", "Diego García", false);
            AgregarActividad(unaH7);
            Hostal unaH8 = new Hostal("Clases de tenis", "Un profesor dará clases de tenis nivel principiante", DateTime.Today.AddDays(-1), 5, 12, "Cancha de tenis", "Andrea Pérez", true, 10);
            AgregarActividad(unaH8);
            Hostal unaH9 = new Hostal("Cine", "Cine al aire libre con proyección de películas populares", DateTime.Today, 50, 3, "Jardín principal", "Alejandro Herrera", true);
            AgregarActividad(unaH9);
            Hostal unaH10 = new Hostal("Excursiones", "Excursiones a parques naturales cercanos", DateTime.Today.AddDays(-2), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            AgregarActividad(unaH10);

            // Prueba Nombre de actividad vacío
            // Mensaje de error: "El nombre de la actividad no puede ser vacío"
            //Hostal unaH11 = new Hostal("", "Excursiones a parques naturales cercanos", new DateTime(2022, 06, 09), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH11);

            // Prueba Cantidad de caracteres en nombre de actividad no mayor a 25 
            // Mensaje de error: "El nombre de la actividad no puede tener más de 25 caracteres"
            //Hostal unaH12 = new Hostal("abcdefghijklmnopqrstuvwxyz1", "Excursiones a parques naturales cercanos", new DateTime(2022, 06, 09), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH12);

            // Prueba Descripción vacía
            // Mensaje de error: "La descripción de la actividad no puede ser vacía"
            //Hostal unaH13 = new Hostal("Excursiones", "", new DateTime(2022, 06, 09), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH13);

            // Prueba Fecha de actividad 
            // Mensaje de error: "La fecha de la actividad debe ser igual o mayor a la fecha de hoy"
            //Hostal unaH14 = new Hostal("Excursiones", "Excursiones a parques naturales cercanos", new DateTime(2022, 06, 09), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH14);

            // Prueba Cupos disponibles
            // Mensaje de error: "La actividad ya no tiene cupos disponibles"
            //Hostal unaH15 = new Hostal("Programacion", "Excursiones a parques naturales cercanos", new DateTime(2023, 06, 09), 0, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH15); 

            // Prueba Nombre de responsable vacío
            // Mensaje de error: "El nombre del responsable no puede ser vacío"
            //Hostal unaH16 = new Hostal("Excursiones", "Excursiones a parques naturales cercanos", new DateTime(2023, 06, 09), 30, 12, "Parques naturales cercanos", "", true, 30);
            //AgregarActividad(unaH16);

            // Prueba Lugar vacío
            // Mensaje de error: "El nombre del lugar no puede ser vacío"
            //Hostal unaH17 = new Hostal("Excursiones", "Excursiones a parques naturales cercanos", new DateTime(2023, 06, 09), 30, 12, "", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH17);

            // Prueba Agregar una actividad ya existente 
            // Mensaje de error: "La actividad con el Id 11 ya está registrada"
            //Hostal unaH18 = new Hostal("Excursiones", "Excursiones a parques naturales cercanos", new DateTime(2023, 06, 09), 30, 12, "Parques naturales cercanos", "Ana Sánchez", true, 30);
            //AgregarActividad(unaH18);
            //Actividad unaA = unaH18;
            //AgregarActividad(unaH18);
        }

        /// <summary>
        /// Método donde se instancian y se agregan las actividades tercerizadas, se instancian también las confirmaciones que se pasan como párametro al constructor de actividad tercerizada
        /// </summary>
        private void PrecargarDatosActividadesTercerizadas()
        {
            //Confirmaciones
            Confirmacion unaC1 = new Confirmacion(_proveedores[0], null);
            Confirmacion unaC2 = new Confirmacion(_proveedores[0], DateTime.Today.AddDays(-3));
            Confirmacion unaC3 = new Confirmacion(_proveedores[0], DateTime.Today.AddDays(-3));
            Confirmacion unaC4 = new Confirmacion(_proveedores[1], DateTime.Today.AddDays(-3));
            Confirmacion unaC5 = new Confirmacion(_proveedores[1], null);
            Confirmacion unaC6 = new Confirmacion(_proveedores[1], DateTime.Today.AddDays(-3));
            Confirmacion unaC7 = new Confirmacion(_proveedores[2], null);
            Confirmacion unaC8 = new Confirmacion(_proveedores[2], DateTime.Today.AddDays(-3));
            Confirmacion unaC9 = new Confirmacion(_proveedores[2], DateTime.Today.AddDays(-3));
            Confirmacion unaC10 = new Confirmacion(_proveedores[3], null);
            Confirmacion unaC11 = new Confirmacion(_proveedores[3], DateTime.Today.AddDays(-3));
            Confirmacion unaC12 = new Confirmacion(_proveedores[3], DateTime.Today.AddDays(-3));
            Confirmacion unaC13 = new Confirmacion(_proveedores[4], null);
            Confirmacion unaC14 = new Confirmacion(_proveedores[4], DateTime.Today.AddDays(-3));
            Confirmacion unaC15 = new Confirmacion(_proveedores[4], DateTime.Today.AddDays(-3));
            Confirmacion unaC16 = new Confirmacion(_proveedores[7], DateTime.Today.AddDays(-3));

            //Actividades Tercerizadas
            Tercerizada unaT1 = new Tercerizada("Alquiler de bicicletas", "Ofrece a los huéspedes la oportunidad de alquilar bicicletas", DateTime.Today, 60, 8, AgregarConfirmacionATercerizada(unaC2), 10);
            AgregarActividad(unaT1);
            Tercerizada unaT2 = new Tercerizada("Clases de surf", "Lecciones de surf en el agua", DateTime.Today, 2, 10, AgregarConfirmacionATercerizada(unaC15), 12);
            AgregarActividad(unaT2);
            Tercerizada unaT3 = new Tercerizada("Alquiler de motos", "Ofrece a los huéspedes la oportunidad de alquilar motos", DateTime.Today.AddDays(-2), 30, 18, AgregarConfirmacionATercerizada(unaC3), 25);
            AgregarActividad(unaT3);
            Tercerizada unaT4 = new Tercerizada("Clase de buceo", "Lecciones de buceo bajo el agua", DateTime.Today, 1, 18, AgregarConfirmacionATercerizada(unaC4), 30);
            AgregarActividad(unaT4);
            Tercerizada unaT5 = new Tercerizada("Senderismo", "Excursiones de senderismo por la montaña", DateTime.Today.AddDays(1), 60, 25, AgregarConfirmacionATercerizada(unaC6), 20);
            AgregarActividad(unaT5);
            Tercerizada unaT6 = new Tercerizada("Paseos en barco", "Excursiones en barco por la costa", DateTime.Today, 0, 15, AgregarConfirmacionATercerizada(unaC6), 25);
            AgregarActividad(unaT6);
            Tercerizada unaT7 = new Tercerizada("Servicios de transporte", "Servicios de transporte local y aeropuerto", DateTime.Today.AddDays(2), 9, 5, AgregarConfirmacionATercerizada(unaC9), 20);
            AgregarActividad(unaT7);
            Tercerizada unaT8 = new Tercerizada("Paseos a caballo", "Excursiones a caballo por la montaña o playa", DateTime.Today.AddDays(2), 30, 15, AgregarConfirmacionATercerizada(unaC8), 15);
            AgregarActividad(unaT8);
            Tercerizada unaT9 = new Tercerizada("Tour de fotografía", "Tours guiados de fotografía por la ciudad o paisajes", DateTime.Today.AddDays(2), 15, 15, AgregarConfirmacionATercerizada(unaC9), 20);
            AgregarActividad(unaT9);
            Tercerizada unaT10 = new Tercerizada("Servicio de traducción", "Servicio de traducción para turistas", DateTime.Today.AddDays(-1), 5, 15, AgregarConfirmacionATercerizada(unaC11), 15);
            AgregarActividad(unaT10);
            Tercerizada unaT11 = new Tercerizada("Tour de compras", "Visitas a centros comerciales y tiendas locales", DateTime.Today.AddDays(-1), 30, 14, AgregarConfirmacionATercerizada(unaC11), 15);
            AgregarActividad(unaT11);
            Tercerizada unaT12 = new Tercerizada("Masajes", "Servicios de masaje en habitación o spa local", DateTime.Today.AddDays(-1), 0, 15, AgregarConfirmacionATercerizada(unaC12), 20);
            AgregarActividad(unaT12);
            Tercerizada unaT13 = new Tercerizada("Tour de historia", "Tours guiados por la historia de la ciudad o región", DateTime.Today.AddDays(2), 25, 15, AgregarConfirmacionATercerizada(unaC12), 20);
            AgregarActividad(unaT13);
            Tercerizada unaT14 = new Tercerizada("Eventos de arte", "Exposiciones y eventos de arte local", DateTime.Today, 40, 50, AgregarConfirmacionATercerizada(unaC14), 30);
            AgregarActividad(unaT14);
            Tercerizada unaT15 = new Tercerizada("Tours en kayak", "Exploración en kayak por la costa", DateTime.Today.AddDays(2), 10, 18, AgregarConfirmacionATercerizada(unaC15), 25);
            AgregarActividad(unaT15);
            Tercerizada unaT16 = new Tercerizada("Excursión en helicóptero", "Vistas panorámicas desde el aire", DateTime.Today.AddDays(2), 1, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            AgregarActividad(unaT16);

            // Prueba Actividad no confirmada por el proveedor
            // Mensaje de error: "La actividad no está confirmada por el proveedor"
            //Tercerizada unaT17 = new Tercerizada("Alquiler de bicicletas", "Ofrece a los huéspedes la oportunidad de alquilar bicicletas", new DateTime(2023, 06, 01), 60, 8, AgregarConfirmacionATercerizada(unaC1), 10);
            //AgregarActividad(unaT17);

            // Prueba Fecha de actividad
            // Mensaje de error: "La fecha de la actividad debe ser igual o mayor a la fecha de hoy"
            //Tercerizada unaT18 = new Tercerizada("Excursión en helicóptero", "Vistas panorámicas desde el aire", new DateTime(2023, 04, 14), 6, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            //AgregarActividad(unaT18);

            // Prueba Objeto Confirmacion null en constructor de Tercerizada
            // Mensaje de error: "La confirmación no puede ser null"
            //Tercerizada unaT19 = new Tercerizada("Excursión en helicóptero", "Vistas panorámicas desde el aire", new DateTime(2023, 08, 14), 6, 18, AgregarConfirmacionATercerizada(null), 50);
            //AgregarActividad(unaT19);

            // Prueba Cupos disponibles
            // Mensaje de error: "La actividad ya no tiene cupos disponibles"
            // Notar que se instancia la actividad "unaT20" con cupos disponibles = 0
            //Tercerizada unaT20 = new Tercerizada("Excursión en helicóptero", "Vistas panorámicas desde el aire", new DateTime(2023, 08, 14), 0, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            //AgregarActividad(unaT20);

            // Prueba Descripción vacía
            // Mensaje de error: "La descripción de la actividad no puede ser vacía"
            //Tercerizada unaT21 = new Tercerizada("Excursión en helicóptero", "", new DateTime(2023, 08, 14), 6, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            //AgregarActividad(unaT21);

            // Prueba Nombre de actividad vacío
            // Mensaje de error: "El nombre de la actividad no puede ser vacío"
            //Tercerizada unaT22 = new Tercerizada("", "Vistas panorámicas desde el aire", new DateTime(2023, 08, 14), 6, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            //AgregarActividad(unaT22);

            // Prueba Cantidad de caracteres en nombre no mayor a 25 
            // Mensaje de error: "El nombre de la actividad no puede tener más de 25 caracteres"
            //Tercerizada unaT23 = new Tercerizada("01234567890123456789123456", "Vistas panorámicas desde el aire", new DateTime(2023, 08, 14), 6, 18, AgregarConfirmacionATercerizada(unaC16), 50);
            //AgregarActividad(unaT23);
        }

        /// <summary>
        /// Método donde se instancian y se agregan las agendas
        /// </summary>
        private void PrecargarDatosAgendas()
        {
            Agenda unaA1 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Tours en kayak"));
            AgregarAgenda(unaA1);
            Agenda unaA2 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Piscina"));
            AgregarAgenda(unaA2);
            Agenda unaA3 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Alquiler de motos"));
            AgregarAgenda(unaA3);
            Agenda unaA4 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Clases de surf"));
            AgregarAgenda(unaA4);
            Agenda unaA5 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Cata de vinos"));
            AgregarAgenda(unaA5);
            Agenda unaA6 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Cine"));
            AgregarAgenda(unaA6);
            Agenda unaA7 = new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Gimnasia"));
            AgregarAgenda(unaA7);
            Agenda unaA8 = new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Clases de baile"));
            AgregarAgenda(unaA8);
            Agenda unaA9 = new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Alquiler de bicicletas"));
            AgregarAgenda(unaA9);
            Agenda unaA10 = new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Tour de fotografía"));
            AgregarAgenda(unaA10);

            // Prueba Validación de cantidad de cupos disponibles para antes de agregar agenda
            // Mensaje de error: "La actividad ya no tiene cupos disponibles"
            // La actividad "Excursión en helicóptero" tiene un cantidad máxima de 1 persona
            //Agenda unaA11 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Excursión en helicóptero"));
            //AgregarAgenda(unaA11);
            //Agenda unaA12 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Excursión en helicóptero"));
            //AgregarAgenda(unaA12);

            // Prueba Agregar un objeto agenda ya existente en la lista
            // Mensaje de error: "La agenda con 'Cecilia López' y actividad 'Tours en kayak' ya está registrada"
            //Agenda unaA14 = new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Tours en kayak"));
            //AgregarAgenda(unaA14);
            //Agenda unaA15= new Agenda((Huesped)_usuarios[1], ObtenerActividadPorNombre("Tours en kayak"));
            //AgregarAgenda(unaA15);

            // Prueba El huésped debe tener la edad mínima para agendarse a la actividad
            // La actividad "Senderismo" tiene una edad mínima de 25 años y los huéspede de la precarga tiene 22
            // Mensaje de error: "El huésped no cuenta con la edad mínima para poder agendarse en la actividad"
            //Agenda unaA16 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("Senderismo"));
            //AgregarAgenda(unaA16);

            //Prueba La actividad debe existir en el sistema
            // Mensaje de error: "La actividad no puede ser null"
            //Agenda unaA17 = new Agenda((Huesped)_usuarios[0], ObtenerActividadPorNombre("AAAAA"));
            //AgregarAgenda(unaA17);
        }

        /// <summary>
        /// Método que realiza validaciones al objeto TipoDocumento que entra como parámetro, verifica que no exista en la lista antes de agregarlo, y finalmente lo agrega a la lista
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarTipoDocumento(TipoDocumento tipoDocumento)
        {
            if (tipoDocumento == null)
            {
                throw new Exception("El tipo de documento recibido no debe ser vacío");
            }
            tipoDocumento.Validar();
            if (_tipoDocumentos.Contains(tipoDocumento))
            {
                throw new Exception($"El tipo de documento {tipoDocumento.Nombre} ya se encuentra en la lista");
            }
            _tipoDocumentos.Add(tipoDocumento);
        }

        /// <summary>
        /// Método que verifica que no sea null el documento y llama al metodo Validar(),
        /// Se usa para pasar por parámetro al constructor de Huesped un objeto Documento validado previamente
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Documento AgregarDocumentoAHuesped(Documento documento)
        {
            if (documento == null)
            {
                throw new Exception("El documento recibido no es válido");
            }
            documento.Validar();
            return documento;
        }

        /// <summary>
        /// Método que realiza validaciones al objeto huesped que entra como parámetro, verifica que no exista en la lista antes de agregarlo, y finalmente lo agrega a la lista
        /// </summary>
        /// <param name="huesped"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarHuesped(Huesped huesped)
        {
            if (huesped == null)
            {
                throw new Exception("El huesped recibido no es vàlido.");
            }
            huesped.Validar();
            ValidarUnicidadEmail(huesped);
            ValidarUnicidadDocumento(huesped);
            _usuarios.Add(huesped);
        }

        /// <summary>
        /// Método que realiza validaciones al objeto operador que entra como parámetro, verifica que no exista en la lista antes de agregarlo, y finalmente lo agrega a la lista
        /// </summary>
        /// <param name="operador"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarOperador(Operador operador)
        {
            if (operador == null)
            {
                throw new Exception("El operador recibido no es válido");
            }
            if (_usuarios.Contains(operador))
            {
                throw new Exception("El operador ya se encuentra en la lista");
            }
            _usuarios.Add(operador);
        }

        /// <summary>
        /// Verifica si la lista de usuarios ya contiene al usuario(si ya existe un usuario en la lista con mismo Email)
        /// </summary>
        /// <param name="usr"></param>
        /// <exception cref="Exception"></exception>
        public void ValidarUnicidadEmail(Usuario usuario)
        {
            if (_usuarios.Contains(usuario))
            {
                throw new Exception($"Ya hay un usuario registrado con el mismo email {usuario.Email}");
            }
        }

        /// <summary>
        /// Verifica si la lista de usuarios ya contiene al huesped(si ya existe un huesped en la lista con el mismo Tipo de documento y mismo Id)
        /// </summary>
        /// <param name="huesped"></param>
        /// <exception cref="Exception"></exception>
        public void ValidarUnicidadDocumento(Huesped huesped)
        {
            if (ListadoHuespedes().Contains(huesped))
            {
                throw new Exception($"Ya hay un huésped registrado con el mismo documento {huesped.Documento.TipoDocumento.Nombre} {huesped.Documento.Numero}");
            }
        }

        /// <summary>
        /// Método que devuelve un listado de todos los objetos huesped que se encuentran en la lista de usuarios.
        /// </summary>
        /// <returns></returns>
        public List<Huesped> ListadoHuespedes()
        {
            List<Huesped> aux = new List<Huesped>();
            foreach (Usuario item in _usuarios)
            {
                if (item is Huesped)
                {
                    aux.Add((Huesped)item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Método que realiza validaciones al objeto proveedor que entra como parámetro, verifica que no exista en la lista antes de agregarlo, y finalmente lo agrega a la lista
        /// </summary>
        /// <param name="proveedor"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new Exception("El proveedor recibido no es vàlido.");
            }
            proveedor.Validar();
            if (_proveedores.Contains(proveedor))
            {
                throw new Exception($"El proveedor con nombre {proveedor.Nombre} ya esta registrado.");
            }
            _proveedores.Add(proveedor);
        }

        /// <summary>
        /// Método que verifica que no sea null el objeto confirmacion y llama al metodo Validar()
        /// Se usa para pasar por parámetro al constructor de Tercerizada un objeto confirmacion validado previamente
        /// </summary>
        /// <param name="confirmacion"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Confirmacion AgregarConfirmacionATercerizada(Confirmacion confirmacion)
        {
            if (confirmacion == null)
            {
                throw new Exception($"La confirmación no puede ser null");
            }
            confirmacion.Validar();
            return confirmacion;
        }

        /// <summary>
        /// Método que realiza validaciones al objeto actividad que entra como parámetro, verifica que no exista en la lista antes de agregarlo, y finalmente lo agrega a la lista
        /// </summary>
        /// <param name="actividad"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarActividad(Actividad actividad)
        {
            if (actividad == null)
            {
                throw new Exception($"La actividad no es válida");
            }
            actividad.Validar();
            if (actividad is Tercerizada)
            {
                Tercerizada unaT = (Tercerizada)actividad;
                unaT.EstaConfirmada();
            }
            if (_actividades.Contains(actividad))
            {
                throw new Exception($"La actividad con el Id {actividad.Id} ya está registrada");
            }
            _actividades.Add(actividad);
        }

        /// <summary>
        /// Método que realiza validaciones al objeto agenda que entra como parámetro, calcula su costofinal y estado, y finalmente agrega la agenda a la lista
        /// </summary>
        /// <param name="agenda"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarAgenda(Agenda agenda)
        {
            if (agenda == null)
            {
                throw new Exception("La agenda recibida no es vàlida.");
            }
            agenda.Validar();
            if (_agendas.Contains(agenda))
            {
                throw new Exception($"La agenda con '{agenda.Huesped.Nombre} {agenda.Huesped.Apellido}' y actividad '{agenda.Actividad.NombreActividad}' ya fue registrada");
            }
            agenda.Actividad.CuposDisponibles--;
            _agendas.Add(agenda);
        }

        /// <summary>
        /// Devuelve un objeto Proveedor que coincide con el nombre que se pasa por parámetro, si no lo encuentra en la lista devuelve null
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Proveedor? ObtenerProveedorPorNombre(string nombre)
        {
            foreach (Proveedor item in _proveedores)
            {
                if (item.Nombre.ToUpper() == nombre.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Devuelve un index de la lista de actividades coincide con el nombre que se pasa por parámetro, si no lo encuentra en la lista devuelve null
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Actividad ObtenerActividadPorNombre(string nombre)
        {
            foreach (Actividad item in _actividades)
            {
                if (item.NombreActividad.ToUpper() == nombre.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Asigna un descuento a un proveedor, antes de asignar el descuento valida los parámetros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descuento"></param>
        /// <exception cref="Exception"></exception>
        public void ModificarDescuentoProveedor(string nombre, decimal descuento)
        {
            if (descuento < 0 || descuento > 100)
            {
                throw new Exception($"El descuento no es válido, debe ser un número entre 0 y 100");
            }
            if (!Utilidades.StringValido(nombre))
            {
                throw new Exception($"El nombre no es vàlido.");
            }
            Proveedor unP = ObtenerProveedorPorNombre(nombre);
            if (unP == null)
            {
                throw new Exception($"No se encontró el proveedor con el nombre '{nombre}'");
            }
            unP.Descuento = descuento;
        }

        /// <summary>
        /// Obtiene una lista de actividades disponibles hoy
        /// </summary>
        /// <returns>Una lista de objetos Actividad que están disponibles hoy</returns>
        public List<Actividad> ListadoActividadesDisponiblesHoy()
        {
            List<Actividad> aux = new List<Actividad>();
            foreach (Actividad item in _actividades)
            {
                if (item.CuposDisponibles > 0 && item.Fecha == DateTime.Today)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Obtiene una lista de todas las actividades de hoy (incluso las que no tienen cupos disponibles)
        /// </summary>
        /// <returns>Una lista de objetos Actividad de hoy</returns>
        public List<Actividad> ListadoActividadesHoy()
        {
            List<Actividad> aux = new List<Actividad>();
            foreach (Actividad item in _actividades)
            {
                if (item.Fecha == DateTime.Today)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Obtiene una lista de actividades disponibles para una fecha específica
        /// </summary>
        /// <param name="fecha">Fecha para la cual se desea obtener las actividades disponibles</param>
        /// <returns>Una lista de actividades disponibles para la fecha especificada</returns>
        public List<Actividad> ListadoActividadesDisponiblesXFecha(DateTime fecha)
        {
            List<Actividad> aux = new List<Actividad>();
            foreach (Actividad item in _actividades)
            {
                if (item.CuposDisponibles > 0 && item.Fecha == fecha)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Obtiene una lista de todas las actividades para una fecha específica (incluso si no tienen cupos disponibles)
        /// </summary>
        /// <param name="fecha">Fecha para la cual se desea obtener las actividades</param>
        /// <returns>Una lista de actividades para la fecha especificada</returns>
        public List<Actividad> ListadoActividadesXFecha(DateTime fecha)
        {
            List<Actividad> aux = new List<Actividad>();
            foreach (Actividad item in _actividades)
            {
                if (item.Fecha == fecha)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        /// <summary>
        /// Obtiene una lista de las agendas programadas para hoy
        /// </summary>
        /// <returns>Lista de agendas programadas para hoy</returns>
        public List<Agenda> ListadoAgendasHoy()
        {
            List<Agenda> aux = new List<Agenda>();
            foreach (Agenda item in _agendas)
            {
                if (item.Actividad.Fecha == DateTime.Today)
                {
                    aux.Add(item);
                }
            }
            aux.Sort();
            return aux;
        }

        /// <summary>
        /// Obtiene una lista de agendas filtrada por fecha
        /// </summary>
        /// <param name="fecha">La fecha para la cual se desea obtener el listado de agendas</param>
        /// <returns>Una lista de agendas que tienen la fecha especificada</returns>
        public List<Agenda> ListadoAgendasXFecha(DateTime fecha)
        {
            List<Agenda> aux = new List<Agenda>();
            foreach (Agenda item in _agendas)
            {
                if (item.Actividad.Fecha == fecha)
                {
                    aux.Add(item);
                }
            }
            aux.Sort();
            return aux;
        }

        /// <summary>
        /// Obtiene una actividad por su ID
        /// </summary>
        /// <param name="id">El ID de la actividad a buscar</param>
        /// <returns>La actividad encontrada o null si no se encuentra ninguna actividad con el ID especificado</returns>
        public Actividad ObtenerActividadPorId(int id)
        {
            foreach (Actividad item in _actividades)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene un tipo de documento por su ID
        /// </summary>
        /// <param name="id">El ID del tipo de documento</param>
        /// <returns>El objeto TipoDocumento correspondiente al ID proporcionado, o null si no se encuentra</returns>
        public TipoDocumento ObtenerTipoDocumentoPorId(int id)
        {
            foreach (TipoDocumento item in _tipoDocumentos)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene una lista de agendas para un huésped específico
        /// </summary>
        /// <param name="huesped">El objeto Huesped para el cual se desea obtener la lista de agendas</param>
        /// <returns>Una lista de objetos Agenda que pertenecen al huésped especificado</returns>
        public List<Agenda> ListadoAgendasXHuesped(Huesped huesped)
        {
            List<Agenda> aux = new List<Agenda>();
            foreach (Agenda item in _agendas)
            {
                if (item.Huesped.Email == huesped.Email)
                {
                    aux.Add(item);
                }
            }
            aux.Sort();
            return aux;
        }

        /// <summary>
        /// Obtiene un usuario basado en su email y contraseña
        /// </summary>
        /// <param name="email">El email del usuario.</param>
        /// <param name="contrasena">La contraseña del usuario</param>
        /// <returns>El usuario correspondiente al email y contraseña proporcionados, o null si no se encuentra</returns>
        public Usuario ObtenerUsuarioXEmailYContrasenia(string email, string contrasena)
        {
            foreach (Usuario item in _usuarios)
            {
                if (item.Contrasena == contrasena && item.Email == email)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene un objeto Huesped basado en la dirección de correo electrónico proporcionada
        /// </summary>
        /// <param name="email">La dirección de correo electrónico del Huesped a buscar</param>
        /// <returns>El objeto Huesped correspondiente a la dirección de correo electrónico proporcionada. Si no se encuentra ninguna coincidencia, se devuelve null</returns>
        public Huesped ObtenerHuespedXEmail(string email)
        {
            foreach (Usuario item in _usuarios)
            {
                if (item is Huesped unH && unH.Email == email)
                {
                    return unH;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene un operador basado en su dirección de correo electrónico
        /// </summary>
        /// <param name="email">La dirección de correo electrónico a buscar</param>
        /// <returns>El objeto Operador correspondiente al correo electrónico proporcionado, o null si no se encuentra</returns>
        public Operador ObtenerOperadorXEmail(string email)
        {
            foreach (Usuario item in _usuarios)
            {
                if (item is Operador unO && unO.Email == email)
                {
                    return unO;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene un objeto Huesped a partir del ID del tipo de documento y el número de documento
        /// </summary>
        /// <param name="idTipoDocumento">El ID del tipo de documento</param>
        /// <param name="numeroDocumento">El número de documento</param>
        /// <returns>El objeto Huesped correspondiente, o null si no se encuentra</returns>
        public Huesped ObtenerHuespedXDocumento(int idTipoDocumento, string numeroDocumento)
        {
            foreach (Usuario item in _usuarios)
            {
                if (item is Huesped unH && unH.Documento.Numero == numeroDocumento && unH.Documento.TipoDocumento.Id == idTipoDocumento)
                {
                    return unH;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene una lista de agendas actuales y futuras para un huésped específico según su dirección de correo electrónico
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del huésped</param>
        /// <returns>Lista de agendas actuales y futuras del huésped</returns>
        public List<Agenda> ListadoAgendasActualesYFuturasDeHuespedXEmail(string email)
        {
            List<Agenda> aux = new List<Agenda>();
            foreach (Agenda item in _agendas)
            {
                if (item.Huesped.Email == email && item.Actividad.Fecha >= DateTime.Today)
                {
                    aux.Add(item);
                }
            }
            aux.Sort();
            return aux;
        }

        /// <summary>
        /// Obtiene una instancia de la clase Agenda basada en el ID proporcionado
        /// </summary>
        /// <param name="agendaID">El ID de la Agenda que se desea obtener</param>
        /// <returns>La Agenda correspondiente al ID proporcionado, o null si no se encuentra ninguna coincidencia</returns>
        public Agenda ObtenerAgendaID(int agendaID)
        {
            foreach (Agenda agenda in _agendas)
            {
                if (agenda.Id == agendaID)
                {
                    return agenda;
                }
            }
            return null;
        }
    }
}
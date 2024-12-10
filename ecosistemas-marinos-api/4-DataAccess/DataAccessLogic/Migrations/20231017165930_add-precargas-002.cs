using BusinessLogic.Entities;
using DataAccessLogic.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

#nullable disable

namespace DataAccessLogic.Migrations
{
    /// <inheritdoc />
    public partial class addprecargas002 : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET LOCK_TIMEOUT 600000;");
            migrationBuilder.InsertData(
            table: "Usuarios",
            columns: new[] { "Alias", "Contrasenia", "ContraseniaCifrada", "FechaIngreso", "Discriminator" },
            values: new object[,]
            {
                { "admin1","Admin.12",BCrypt.Net.BCrypt.HashPassword("Admin.12"), new DateTime(2022, 10, 6), "Administrador" },
                { "persona1","Persona.1",BCrypt.Net.BCrypt.HashPassword("Persona.1"), new DateTime(2022, 10, 7), "Persona" },
                { "persona2","Persona.2",BCrypt.Net.BCrypt.HashPassword("Password.2"), new DateTime(2022, 10, 8), "Persona" }
            });

            migrationBuilder.InsertData(
            table: "EstadosDeConservacion",
            columns: new[] { "Id", "Nombre", "RangoConservacion_Minimo", "RangoConservacion_Maximo" },
            values: new object[,]
            {
                { 1, "Malo", 1, 50 },
                { 2, "Aceptable", 51, 70 },
                { 3, "Bueno",71, 90 },
                { 4, "Optimo", 91, 100 }
            });

            migrationBuilder.InsertData(
            table: "Amenazas",
            columns: new[] { "Id", "Descripcion", "GradoDePeligro" },
            values: new object[,]
            {
                { 1, "Amenaza 1",1  },
                { 2, "Amenaza 2", 5 },
                { 3, "Amenaza 3",7 },
                { 4, "Amenaza 4", 5 },
                { 5, "Amenaza 5", 8 },
                { 6, "Amenaza 6", 6 },
                { 7, "Amenaza 7", 3 },
                { 8, "Amenaza 8", 2 },
                { 9, "Amenaza 9", 10 },
                { 10, "Amenaza 10", 1 },
                { 11, "Amenaza 11", 2 },
                { 12, "Amenaza 12", 4 },
                { 13, "Amenaza 13", 9 },
                { 14, "Amenaza 14", 3 },
                { 15, "Amenaza 15", 10 },
                { 16, "Amenaza 16", 10 },
                { 17, "Amenaza 17", 10 }
            });

            //instacio el metodo que llama a la api 
            var loader = new ApiService();

            // obtengo los datos
            var paises = loader.ObtenerPaisesDeApi();

            if (paises != null ) 
            {
                int i = 1;
                foreach (var pais in paises.OrderBy(p => p.Nombre))
                {
                    Console.WriteLine(pais.CodigoIso);
                    migrationBuilder.InsertData(
                        table: "Paises",
                        columns: new[] { "Id", "Nombre", "CodigoIso" },
                        values: new object[] { i, pais.Nombre, pais.CodigoIso }
                    );
                    i++;
                }
            }
            else
            {
                migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "Id", "Nombre", "CodigoIso" },
                values: new object[,]
                {
                    { 1, "Afganistán", "AF" },
                    { 2, "Albania", "AL" },
                    { 3, "Alemania", "DE" },
                    { 4, "Andorra", "AD" },
                    { 5, "Angola", "AO" },
                    { 6, "Antigua y Barbuda", "AG" },
                    { 7, "Arabia Saudita", "SA" },
                    { 8, "Argelia", "DZ" },
                    { 9, "Argentina", "AR" },
                    { 10, "Armenia", "AM" },
                    { 11, "Australia", "AU" },
                    { 12, "Austria", "AT" },
                    { 13, "Azerbaiyán", "AZ" },
                    { 14, "Bahamas", "BS" },
                    { 15, "Bangladés", "BD" },
                    { 16, "Barbados", "BB" },
                    { 17, "Baréin", "BH" },
                    { 18, "Bélgica", "BE" },
                    { 19, "Belice", "BZ" },
                    { 20, "Benín", "BJ" },
                    { 21, "Bielorrusia", "BY" },
                    { 22, "Birmania", "MM" },
                    { 23, "Bolivia", "BO" },
                    { 24, "Bosnia y Herzegovina", "BA" },
                    { 25, "Botsuana", "BW" },
                    { 26, "Brasil", "BR" },
                    { 27, "Brunéi", "BN" },
                    { 28, "Bulgaria", "BG" },
                    { 29, "Burkina Faso", "BF" },
                    { 30, "Burundi", "BI" },
                    { 31, "Bután", "BT" },
                    { 32, "Cabo Verde", "CV" },
                    { 33, "Camboya", "KH" },
                    { 34, "Camerún", "CM" },
                    { 35, "Canadá", "CA" },
                    { 36, "Catar", "QA" },
                    { 37, "Chad", "TD" },
                    { 38, "Chile", "CL" },
                    { 39, "China", "CN" },
                    { 40, "Chipre", "CY" },
                    { 41, "Ciudad del Vaticano", "VA" },
                    { 42, "Colombia", "CO" },
                    { 43, "Comoras", "KM" },
                    { 44, "Corea del Norte", "KP" },
                    { 45, "Corea del Sur", "KR" },
                    { 46, "Costa de Marfil", "CI" },
                    { 47, "Costa Rica", "CR" },
                    { 48, "Croacia", "HR" },
                    { 49, "Cuba", "CU" },
                    { 50, "Dinamarca", "DK" },
                    { 51, "Dominica", "DM" },
                    { 52, "Ecuador", "EC" },
                    { 53, "Egipto", "EG" },
                    { 54, "El Salvador", "SV" },
                    { 55, "Emiratos Árabes Unidos", "AE" },
                    { 56, "Eritrea", "ER" },
                    { 57, "Eslovaquia", "SK" },
                    { 58, "Eslovenia", "SI" },
                    { 59, "España", "ES" },
                    { 60, "Estados Unidos", "US" },
                    { 61, "Estonia", "EE" },
                    { 62, "Etiopía", "ET" },
                    { 63, "Filipinas", "PH" },
                    { 64, "Finlandia", "FI" },
                    { 65, "Fiyi", "FJ" },
                    { 66, "Francia", "FR" },
                    { 67, "Gabón", "GA" },
                    { 68, "Gambia", "GM" },
                    { 69, "Georgia", "GE" },
                    { 70, "Ghana", "GH" },
                    { 71, "Granada", "GD" },
                    { 72, "Grecia", "GR" },
                    { 73, "Guatemala", "GT" },
                    { 74, "Guinea", "GN" },
                    { 75, "Guinea-Bisáu", "GW" },
                    { 76, "Guinea Ecuatorial", "GQ" },
                    { 77, "Guyana", "GY" },
                    { 78, "Haití", "HT" },
                    { 79, "Honduras", "HN" },
                    { 80, "Hungría", "HU" },
                    { 81, "India", "IN" },
                    { 82, "Indonesia", "ID" },
                    { 83, "Irak", "IQ" },
                    { 84, "Irán", "IR" },
                    { 85, "Irlanda", "IE" },
                    { 86, "Islandia", "IS" },
                    { 87, "Islas Marshall", "MH" },
                    { 88, "Islas Salomón", "SB" },
                    { 89, "Israel", "IL" },
                    { 90, "Italia", "IT" },
                    { 91, "Jamaica", "JM" },
                    { 92, "Japón", "JP" },
                    { 93, "Jordania", "JO" },
                    { 94, "Kazajistán", "KZ" },
                    { 95, "Kenia", "KE" },
                    { 96, "Kirguistán", "KG" },
                    { 97, "Kiribati", "KI" },
                    { 98, "Kuwait", "KW" },
                    { 99, "Laos", "LA" },
                    { 100, "Lesoto", "LS" },
                    { 101, "Letonia", "LV" },
                    { 102, "Líbano", "LB" },
                    { 103, "Liberia", "LR" },
                    { 104, "Libia", "LY" },
                    { 105, "Liechtenstein", "LI" },
                    { 106, "Lituania", "LT" },
                    { 107, "Luxemburgo", "LU" },
                    { 108, "Macedonia del Norte", "MK" },
                    { 109, "Madagascar", "MG" },
                    { 110, "Malasia", "MY" },
                    { 111, "Malaui", "MW" },
                    { 112, "Maldivas", "MV" },
                    { 113, "Malí", "ML" },
                    { 114, "Malta", "MT" },
                    { 115, "Marruecos", "MA" },
                    { 116, "Mauricio", "MU" },
                    { 117, "Mauritania", "MR" },
                    { 118, "México", "MX" },
                    { 119, "Micronesia", "FM" },
                    { 120, "Moldavia", "MD" },
                    { 121, "Mónaco", "MC" },
                    { 122, "Mongolia", "MN" },
                    { 123, "Montenegro", "ME" },
                    { 124, "Mozambique", "MZ" },
                    { 125, "Namibia", "NA" },
                    { 126, "Nauru", "NR" },
                    { 127, "Nepal", "NP" },
                    { 128, "Nicaragua", "NI" },
                    { 129, "Níger", "NE" },
                    { 130, "Nigeria", "NG" },
                    { 131, "Noruega", "NO" },
                    { 132, "Nueva Zelanda", "NZ" },
                    { 133, "Omán", "OM" },
                    { 134, "Países Bajos", "NL" },
                    { 135, "Pakistán", "PK" },
                    { 136, "Palaos", "PW" },
                    { 137, "Panamá", "PA" },
                    { 138, "Papúa Nueva Guinea", "PG" },
                    { 139, "Paraguay", "PY" },
                    { 140, "Perú", "PE" },
                    { 141, "Polonia", "PL" },
                    { 142, "Portugal", "PT" },
                    { 143, "Reino Unido", "GB" },
                    { 144, "República Centroafricana", "CF" },
                    { 145, "República Checa", "CZ" },
                    { 146, "República del Congo", "CG" },
                    { 147, "República Democrática del Congo", "CD" },
                    { 148, "República Dominicana", "DO" },
                    { 149, "Ruanda", "RW" },
                    { 150, "Rumanía", "RO" },
                    { 151, "Rusia", "RU" },
                    { 152, "Samoa", "WS" },
                    { 153, "San Cristóbal y Nieves", "KN" },
                    { 154, "San Marino", "SM" },
                    { 155, "San Vicente y las Granadinas", "VC" },
                    { 156, "Santa Lucía", "LC" },
                    { 157, "Santo Tomé y Príncipe", "ST" },
                    { 158, "Senegal", "SN" },
                    { 159, "Serbia", "RS" },
                    { 160, "Seychelles", "SC" },
                    { 161, "Sierra Leona", "SL" },
                    { 162, "Singapur", "SG" },
                    { 163, "Siria", "SY" },
                    { 164, "Somalia", "SO" },
                    { 165, "Sri Lanka", "LK" },
                    { 166, "Suazilandia", "SZ" },
                    { 167, "Sudáfrica", "ZA" },
                    { 168, "Sudán", "SD" },
                    { 169, "Sudán del Sur", "SS" },
                    { 170, "Suecia", "SE" },
                    { 171, "Suiza", "CH" },
                    { 172, "Surinam", "SR" },
                    { 173, "Tailandia", "TH" },
                    { 174, "Taiwán", "TW" },
                    { 175, "Tanzania", "TZ" },
                    { 176, "Tayikistán", "TJ" },
                    { 177, "Timor Oriental", "TL" },
                    { 178, "Togo", "TG" },
                    { 179, "Tonga", "TO" },
                    { 180, "Trinidad y Tobago", "TT" },
                    { 181, "Túnez", "TN" },
                    { 182, "Turkmenistán", "TM" },
                    { 183, "Turquía", "TR" },
                    { 184, "Tuvalu", "TV" },
                    { 185, "Ucrania", "UA" },
                    { 186, "Uganda", "UG" },
                    { 187, "Uruguay", "UY" },
                    { 188, "Uzbekistán", "UZ" },
                    { 189, "Vanuatu", "VU" },
                    { 190, "Venezuela", "VE" },
                    { 191, "Vietnam", "VN" },
                    { 192, "Yemen", "YE" },
                    { 193, "Yibuti", "DJ" },
                    { 194, "Zambia", "ZM" },
                    { 195, "Zimbabue", "ZW" }
                 });
            }



        migrationBuilder.InsertData(
        table: "Ecosistemas",
        columns: new[] { "Id", "Nombre", "Area", "Descripcion", "EstaActivo", "EstadoDeConservacionId", "Ubicacion_Latitud", "Ubicacion_Longitud" },
        values: new object[,]
        {
                { 1, "Arrecife Coralino Alpha", 150, "Un vibrante ecosistema submarino, el Arrecife Coralino Alpha es hogar de miles de especies marinas.", true, 1, -34.9000, 138.6000 },
                { 2, "Banco de Arena Echo", 275, "Conocido por sus aguas poco profundas, el Banco de Arena Echo es crucial para la biodiversidad marina.", true, 2, 32.3078, -64.7505 },
                { 3, "Fosa Marina Sierra", 340, "La Fosa Marina Sierra, profunda y misteriosa, es un ecosistema crítico para la investigación oceánica.", true, 3, 11.0000, 142.2000 },
                { 4, "Bosque de Algas Bravo", 220, "Este ecosistema es esencial para muchas especies. El Bosque de Algas Bravo es un refugio en aguas turbulentas.", true, 4, 28.2916, -16.6291 },
                { 5, "Cuenca Oceánica Delta", 305, "La Cuenca Oceánica Delta destaca por su diversidad de flora y fauna en el lecho marino.", true, 1, -19.2576, 57.6300 },
                { 6, "Plataforma Continental Charlie", 410, "Rica en nutrientes, la Plataforma Continental Charlie es vital para la cadena alimentaria marina.", true, 2, -58.3816, -62.5841 },
                { 7, "Monte Submarino Foxtrot", 365, "El Monte Submarino Foxtrot es un pico submarino biodiverso, esencial para especies migratorias.", true, 3, -54.2800, 158.2343 },
                { 8, "Ventilas Hidrotermales Golf", 190, "Conocidas por su calor extremo, las Ventilas Hidrotermales Golf son un hábitat único para la vida marina.", true, 4, 13.4325, -16.7122 },
                { 9, "Pradera de Pastos Marinos Hotel", 260, "La Pradera de Pastos Marinos Hotel es un ecosistema marino vital que ayuda a la purificación del agua.", true, 1, -45.1000, 167.0000 },
                { 10, "Punto Frío Índigo", 300, "El Punto Frío Índigo, conocido por sus bajas temperaturas, es crucial para la investigación climática marina.", true, 2, -69.0000, 39.7500 }
        });

            migrationBuilder.InsertData(
            table: "Especies",
            columns: new[] { "Id", "NombreCientifico", "NombreVulgar", "Descripcion", "HabitaId", "RangoPeso_Max", "RangoPeso_Min", "RangoLargo_Max", "RangoLargo_Min", "EstadoConservacionId" },
            values: new object[,]
            {
                { 1, "Carcharodon carcharias", "Tiburón blanco", "El tiburón blanco es el depredador más grande del océano y puede llegar a medir hasta 6 metros de largo.", 1, 1900, 680, 6, 4, 1 },
                { 2, "Delphinus delphis", "Delfín común", "El delfín común es muy sociable y a menudo se le ve en grupos jugando cerca de la superficie.", 2, 150, 80, 2.5, 1.7, 1 },
                { 3, "Chelonia mydas", "Tortuga verde", "La tortuga verde se encuentra en mares tropicales y subtropicales alrededor del mundo.", 3, 190, 100, 1.5, 0.8, 3 },
                { 4, "Aurelia aurita", "Medusa luna", "Esta medusa es reconocible por su forma de campana translúcida y puede llegar a medir hasta 40 cm de diámetro.", 4, 0.5, 0.1, 0.4, 0.15, 2 },
                { 5, "Hippocampus erectus", "Caballito de mar", "El caballito de mar es un pequeño pez marino que se caracteriza por su apariencia similar a un caballo.", 5, 0.015, 0.005, 0.15, 0.1, 1 },
                { 6, "Manta birostris", "Manta gigante", "La manta gigante es la especie de raya más grande del mundo y puede llegar a medir hasta 7 metros de envergadura.", 6, 1350, 1000, 7, 4, 2 },
                { 7, "Octopus vulgaris", "Pulpo común", "El pulpo común es un molusco cefalópodo conocido por su inteligencia y habilidad para camuflarse.", 7, 15, 3, 1, 0.5, 2 },
                { 8, "Gadus morhua", "Bacalao", "El bacalao es un pez que habita en aguas frías del Atlántico norte y es muy apreciado por su carne.", 8, 100, 12, 2, 0.5, 3 },
                { 9, "Sphyraena barracuda", "Barracuda", "El barracuda es un pez depredador conocido por su cuerpo largo y su apariencia feroz.", 9, 50, 0.5, 1.8, 0.5, 1 },
                { 10, "Stegastes partitus", "Pez damisela", "El pez damisela es un pequeño pez tropical conocido por sus colores brillantes.", 10, 0.05, 0.01, 0.15, 0.1, 2 },
                { 11, "Pterois volitans", "Pez león", "El pez león es conocido por sus aletas espinosas venenosas y su apariencia llamativa.", 9, 1, 0.5, 0.4, 0.2, 1 },
                { 12, "Acanthurus lineatus", "Pez cirujano", "El pez cirujano es conocido por la pequeña espina afilada en su aleta caudal que utiliza para defenderse.", 8, 1, 0.2, 0.3, 0.2, 3 },
                { 13, "Balistoides viridescens", "Pez ballesta", "El pez ballesta es un pez tropical conocido por sus colores brillantes y su capacidad para inflarse cuando se siente amenazado.", 5, 3, 1, 0.5, 0.3, 1 },
                { 14, "Carcharhinus limbatus", "Tiburón punta negra", "El tiburón punta negra es un tiburón de tamaño mediano que se encuentra comúnmente en aguas tropicales y subtropicales.", 4, 120, 40, 2.5, 1.5, 4 },
                { 15, "Echeneis naucrates", "Pez rémora", "El pez rémora es conocido por adherirse a otros animales marinos más grandes, como tiburones y tortugas, utilizando su aleta dorsal modificada.", 6, 1, 0.2, 0.8, 0.3, 2 }
            });

            migrationBuilder.InsertData(
            table: "Ecosistema_Pais",
            columns: new[] { "EcosistemasId", "PaisesId" },
            values: new object[,]
            {
                { 1, 4},
                { 1, 10},
                { 1, 5},
                { 1, 11},
                { 1, 13},
                { 1, 25},
                { 2, 50},
                { 2, 51},
                { 2, 52},
                { 2, 53},
                { 2, 45},
                { 2, 46},
                { 3, 6},
                { 3, 100},
                { 3, 150},
                { 4, 32},
                { 4, 33},
                { 4, 39},
                { 4, 96},
                { 5, 65},
                { 5, 66},
                { 6, 172},
                { 6, 89},
                { 6, 101},
                { 6, 96},
                { 7, 85},
                { 7, 86},
                { 7, 87},
                { 7, 88},
                { 9, 99},
                { 9, 123},
                { 9, 158},
                { 10, 72},
                { 10, 160},
            });

            migrationBuilder.InsertData(
            table: "AmenazaEcosistema",
            columns: new[] { "EcosistemasId", "AmenazasId" },
            values: new object[,]
            {
                { 1,1 },
                { 1,2 },
                { 2,3 },
                { 2,4 },
                { 3,5 },
                { 4,6 },
                { 5,7 },
                { 5, 8},
                { 6, 9},
                { 7, 10},
                { 8, 11},
                { 8, 12},
                { 9, 13},
                { 10, 14},
                { 10, 15},
            });
            migrationBuilder.InsertData(
            table: "AmenazaEspecie",
            columns: new[] { "EspeciesId", "AmenazasId" },
            values: new object[,]
            {
                { 15,1 },
                { 15,2 },
                { 14,3 },
                { 14,4 },
                { 13,5 },
                { 12,6 },
                { 12,7 },
                { 11, 8},
                { 11, 9},
                { 10, 10},
                { 9, 11},
                { 9, 12},
                { 8, 13},
                { 8, 14},
                { 7, 15},
                { 6, 1},
                { 6, 2},
                { 5, 3},
                { 5, 4},
                { 4, 5},
                { 4, 6},
                { 3, 7},
                { 3, 8},
                { 2, 9},
                { 2, 10},
                { 1, 11},
                { 1, 12},
            });

            migrationBuilder.InsertData(
            table: "Ecosistema_Especie",
            columns: new[] { "EspeciesQuePuedenHabitarloId", "PuedeHabitarId" },
            values: new object[,]
            {
                { 1, 1},
                { 1, 2},
                { 1, 3},
                { 2, 2},
                { 2, 4},
                { 2, 5},
                { 3, 3},
                { 3, 6},
                { 3, 7},
                { 4, 4},
                { 4, 8},
                { 4, 9},
                { 5, 5},
                { 5, 10},
                { 6, 6},
                { 6, 9},
                { 7, 7},
                { 7, 8},
                { 8, 8},
                { 8, 9},
                { 9, 9},
                { 9, 3},
                { 10, 10},
                { 10, 2},
                { 10, 5},
                { 11, 9},
                { 11, 6},
                { 11, 8},
                { 12, 8},
                { 12, 9},
                { 12, 6},
                { 13, 5},
                { 13, 8},
                { 13, 7},
                { 14, 4},
                { 14, 9},
                { 15, 6},
                { 15, 2},
            });

            migrationBuilder.InsertData(
            table: "ImagenEcosistema",
            columns: new[] { "Nombre", "EcosistemaId" },
            values: new object[,]
            {
                { "/ImagenesEcosistema/1_001.png",1 },
                { "/ImagenesEcosistema/1_002.png",1 },
                { "/ImagenesEcosistema/1_003.png",1 },
                { "/ImagenesEcosistema/1_004.png",1 },
                { "/ImagenesEcosistema/2_001.png",2 },
                { "/ImagenesEcosistema/2_002.png",2 },
                { "/ImagenesEcosistema/3_001.png",3 },
                { "/ImagenesEcosistema/3_002.png",3 },
                { "/ImagenesEcosistema/3_003.png",3 },
                { "/ImagenesEcosistema/4_001.png",4 },
                { "/ImagenesEcosistema/4_002.png",4 },
                { "/ImagenesEcosistema/5_001.png",5 },
                { "/ImagenesEcosistema/5_002.png",5 },
                { "/ImagenesEcosistema/6_001.png",6 },
                { "/ImagenesEcosistema/6_002.png",6 },
                { "/ImagenesEcosistema/6_003.png",6 },
                { "/ImagenesEcosistema/7_001.png",7 },
                { "/ImagenesEcosistema/7_002.png",7 },
                { "/ImagenesEcosistema/8_001.png",8 },
                { "/ImagenesEcosistema/8_002.png",8 },
                { "/ImagenesEcosistema/8_003.png",8 },
                { "/ImagenesEcosistema/9_001.png",9 },
                { "/ImagenesEcosistema/9_002.png",9 },
                { "/ImagenesEcosistema/10_001.png",10 },
                { "/ImagenesEcosistema/10_002.png",10 },
            });
            migrationBuilder.InsertData(
            table: "ImagenEspecie",
            columns: new[] { "Nombre", "EspecieId" },
            values: new object[,]
            {
                { "/ImagenesEspecies/1_001.png",1 },
                { "/ImagenesEspecies/1_002.png",1 },
                { "/ImagenesEspecies/1_003.png",1 },
                { "/ImagenesEspecies/1_004.png",1 },
                { "/ImagenesEspecies/2_001.png",2 },
                { "/ImagenesEspecies/2_002.png",2 },
                { "/ImagenesEspecies/3_001.png",3 },
                { "/ImagenesEspecies/3_002.png",3 },
                { "/ImagenesEspecies/3_003.png",3 },
                { "/ImagenesEspecies/4_001.png",4 },
                { "/ImagenesEspecies/4_002.png",4 },
                { "/ImagenesEspecies/5_001.png",5 },
                { "/ImagenesEspecies/5_002.png",5 },
                { "/ImagenesEspecies/6_001.png",6 },
                { "/ImagenesEspecies/6_002.png",6 },
                { "/ImagenesEspecies/6_003.png",6 },
                { "/ImagenesEspecies/7_001.png",7 },
                { "/ImagenesEspecies/7_002.png",7 },
                { "/ImagenesEspecies/8_001.png",8 },
                { "/ImagenesEspecies/8_002.png",8 },
                { "/ImagenesEspecies/8_003.png",8 },
                { "/ImagenesEspecies/9_001.png",9 },
                { "/ImagenesEspecies/9_002.png",9 },
                { "/ImagenesEspecies/10_001.png",10 },
                { "/ImagenesEspecies/10_002.png",10 },
                { "/ImagenesEspecies/11_001.png",11 },
                { "/ImagenesEspecies/11_002.png",11 },
                { "/ImagenesEspecies/12_001.png",12 },
                { "/ImagenesEspecies/12_002.png",12 },
                { "/ImagenesEspecies/13_001.png",13 },
                { "/ImagenesEspecies/13_002.png",13 },
                { "/ImagenesEspecies/14_001.png",14 },
                { "/ImagenesEspecies/14_002.png",14 },
                { "/ImagenesEspecies/15_001.png",15 },
                { "/ImagenesEspecies/15_002.png",15 },
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

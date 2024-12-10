let latitudeOrigen;
let longitudeOrigen;
let map;
// let connection;
const selectDepartamentos = document.querySelector("#slcDepartamentos");
const selectCiudades = document.querySelector("#slcCiudades");
const selectOcupaciones = document.querySelector("#slcOcupaciones");
const selectFiltroOcupaciones = document.querySelector("#slcFiltroOcupacion");
const ruteo = document.querySelector("#ruteo");
inicializar();
AgregarEventos();


function AgregarEventos() {
    // Capacitor.Plugins.Network.addListener('networkStatusChange', networkStatusChange);
    ruteo.addEventListener("ionRouteWillChange", NavegarEntrePaginas);
    document.querySelector("#btnLogin").addEventListener("click", Login)
    document.querySelector("#btnRegistro").addEventListener("click", Registro)
    document.querySelector("#slcDepartamentos").addEventListener("ionChange", ObtenerYCargarCiudadesXDepartamento);
    document.querySelector("#dateFechaNac").addEventListener("ionInput", CargarSelectOcupacionesXEdad);
    document.querySelector("#btnAgregarPersona").addEventListener("click", AgregarPersona);
    document.querySelector("#btnMapaCiudades").addEventListener("click", ObtenerUbicacion);
    document.querySelector("#slcFiltroOcupacion").addEventListener("ionChange", ListarPersonasXOcupacion);
    document.querySelector("#btnListarTodasPersonas").addEventListener("click", function () {
        selectFiltroOcupaciones.value = undefined;
        ListarTodasPersonas();
    });
}


// function networkStatusChange(status) {
//     if (status.connectionType === "none") {
//         connection = false;
//         MostrarMensaje("No tienes conexión a internet")
//     } else {
//         connection = true;
//        MostrarMensaje("Conexión a Internet restaurada");
//     }
// }


// async function checkNetworkStatus() {
//     const status = await Capacitor.Plugins.Network.getStatus();

//     if (status.connectionType === "none") {
//         connection = false;
//         MostrarMensaje("No tienes conexión a internet");
//     }
// }

function NavegarEntrePaginas(event) {
    OcultarPaginas();
    switch (event.detail.to) {
        case "/":
            document.querySelector("#pageInicio").style.display = "block";
            break;
        case "/Login":
            LimpiarCamposLogin();
            document.querySelector("#pageLogin").style.display = "block";
            break;
        case "/Registro":
            LimpiarCamposRegistro();
            document.querySelector("#pageRegistro").style.display = "block";
            break;
        case "/Censar":
            LimpiarCamposAgregarPersona();
            ObtenerYCargarDepartamentos();
            document.querySelector("#pageCensar").style.display = "block";
            break;
        case "/ListaPersonas":
            ListarTodasPersonas();
            selectFiltroOcupaciones.value = undefined;
            CargarSelectOcupaciones("slcFiltroOcupacion")
            document.querySelector("#pageListaPersonas").style.display = "block";
            break;
        case "/ListaTotales":
            ListarCantPersonasMvdInteriorYTotales();
            document.querySelector("#pageListaTotales").style.display = "block";
            break;
        case "/Mapa":
            LimpiarCampoDistanciaMapa();
            //se obtiene la ubicación cada vez que el usuario va a ver el mapa para tener actualizada su ubicación dado que podría estar traslandandose
            //y si se tomara la ubicación al abrir la aplicación y el usuario estuviera viajando en ruta por ejemplo, la ubicación podría cambiar varios km en minutos
            ObtenerUbicacion();
            document.querySelector("#pageMapa").style.display = "block";
            break;
        case "/CerrarSesion":
            CerrarSesion();
            break;
    }
}


function OcultarPaginas() {
    let paginas = document.querySelectorAll("ion-page");
    paginas.forEach(pagina => {
        pagina.style.display = "none";
    });
}


function CerrarMenu() {
    document.querySelector("#menu").close();
}


function inicializar() {
    // checkNetworkStatus();
    if (localStorage.getItem("apikey") == null) {
        Inicio("noLogueado");
    } else {
        Inicio("Logueado");
    }
}


function Inicio(usuario) {
    MostrarBotones(usuario);
    if (usuario == "Logueado") {
        ruteo.push("/");
    } else {
        ruteo.push("/Login");
    }
}


function CerrarSesion() {
    localStorage.clear();
    Inicio("noLogueado");
}


function RedirigirSesionExpirada(mensaje) {
    //se borra el localStorage porque si abre y cierra a pesar de haber sido redirigido, 
    //cuando abra de nuevo la aplicación se ejecuta Incializar(). Inicializar() va a preguntar si hay una apikey, y si hay una en localStorage va a tomar como que hay una sesión 
    //y le va a volver a mostrar el menu de usuario logueado de vuelta
    CerrarSesion();
    MostrarMensaje(mensaje);
}


function MostrarBotones(usuario) {
    OcultarBotones();
    let botonesMostrar = document.querySelectorAll("." + usuario)
    botonesMostrar.forEach(btn => {
        btn.style.display = "inline";
    });
}

function OcultarBotones() {
    let botones = document.querySelectorAll(".btn");
    botones.forEach(btn => {
        btn.style.display = "none";
    });
}


function MostrarMensaje(mensaje) {
    let element = document.createElement("ion-toast");
    element.message = mensaje;
    element.duration = 2600;
    document.body.appendChild(element);
    return element.present();
}


function LimpiarCamposLogin() {
    document.querySelector("#txtUsuarioLogin").value = "";
    document.querySelector("#passLogin").value = "";
}


function LimpiarCamposRegistro() {
    document.querySelector("#passRegistro").value = "";
    document.querySelector("#txtUsuarioRegistro").value = "";
}


function LimpiarCampoDistanciaMapa() {
    document.querySelector("#numDistancia").value = "";
}


function ManejarError(error) {
    if (error.message == "Tu sesión ha expirado") {
        RedirigirSesionExpirada(error.message);
    } else {
        MostrarMensaje(error.message);
    }
}


function ManejarRespuesta(response) {
    if (response.status == 200) {
        return response.json();
    }
    else if (response.status == 401) {
        throw new Error("Tu sesión ha expirado");
    }
    else if (response.status == 500) {
        throw new Error("Error del servidor");
    }
}


function Registro() {
    // if (connection) {
        let usuario = document.querySelector("#txtUsuarioRegistro").value.trim();
        let password = document.querySelector("#passRegistro").value.trim();
        try {
            ValidarDatosRegistroYLogin(usuario, password);
            fetch("https://censo.develotion.com/usuarios.php", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ "usuario": usuario, "password": password }),
            })
                .then(function (response) {
                    if (response.status == 200) {
                        return response.json();
                    }
                    else if (response.status == 409) {
                        throw new Error("Ya existe un usuario registrado con ese nombre");
                    }
                    else if (response.status == 500) {
                        throw new Error("Error del servidor");
                    }
                })
                .then(function (datos) {
                    //apikey e iduser se guarda para autologin
                    localStorage.setItem("apikey", datos.apiKey);
                    localStorage.setItem("iduser", datos.id);
                    Inicio("Logueado");
                    //mensaje en caso exitoso
                    MostrarMensaje("Registro exitoso");
                })
                .catch(function (error) {
                    MostrarMensaje(error.message);
                })
        } catch (Error) {
            MostrarMensaje(Error.message);
        }
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function ValidarDatosRegistroYLogin(usuario, password) {
    if (usuario == "") {
        throw new Error("Debes ingresar un nombre de usuario")
    }
    if (password == "") {
        throw new Error("Debes ingresar una contraseña")
    }
}


function Login() {
    // if (connection) {
        let usuario = document.querySelector("#txtUsuarioLogin").value.trim();
        let password = document.querySelector("#passLogin").value.trim();
        try {
            ValidarDatosRegistroYLogin(usuario, password);
            fetch(" https://censo.develotion.com/login.php", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ "usuario": usuario, "password": password }),
            })
                .then(function (response) {
                    if (response.status == 200) {
                        return response.json();
                    }
                    else if (response.status == 409) {
                        throw new Error("Usuario y/o contraseña incorrectos");
                    }
                    else if (response.status == 500) {
                        throw new Error("Error del servidor");
                    }
                })
                .then(function (datos) {
                    localStorage.setItem("apikey", datos.apiKey);
                    localStorage.setItem("iduser", datos.id);
                    Inicio("Logueado")
                })
                .catch(function (error) {
                    MostrarMensaje(error.message);
                })
        } catch (Error) {
            MostrarMensaje(Error.message);
        }
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function AgregarPersona() {
    // if (connection) {
        let nombre = document.querySelector("#txtNombrePersona").value.trim();
        let departamento = document.querySelector("#slcDepartamentos").value;
        let ciudad = document.querySelector("#slcCiudades").value;
        let fechaNacimiento = document.querySelector("#dateFechaNac").value;
        let ocupacion = document.querySelector("#slcOcupaciones").value;
        try {
            ValidarDatosPersona(nombre, departamento, ciudad, fechaNacimiento, ocupacion);
            fetch("https://censo.develotion.com/personas.php", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "apikey": localStorage.getItem("apikey"),
                    "iduser": localStorage.getItem("iduser")
                },
                body: JSON.stringify({
                    "idUsuario": localStorage.getItem("iduser"),
                    "nombre": nombre,
                    "departamento": departamento,
                    "ciudad": ciudad,
                    "fechaNacimiento": fechaNacimiento,
                    "ocupacion": ocupacion
                }),
            })
                .then(ManejarRespuesta)
                .then(function (datos) {
                    LimpiarCamposAgregarPersona();
                    MostrarMensaje(datos.mensaje);
                })
                .catch(ManejarError)
        } catch (Error) {
            MostrarMensaje(Error.message);
        }
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function LimpiarCamposAgregarPersona() {
    document.querySelector("#txtNombrePersona").value = "";
    document.querySelector("#dateFechaNac").value = "";
    selectOcupaciones.value = undefined;
    selectCiudades.value = undefined;
    selectDepartamentos.value = undefined;
}


function ValidarDatosPersona(nombre, departamento, ciudad, fechaNacimiento, ocupacion) {
    if (nombre == "") {
        throw new Error("Debes ingresar un nombre")
    }
    //controla usando regular expressions que solo se ingresen caracteres esperados en un nombre
    if (!/^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/.test(nombre)) {
        throw new Error("El nombre solo puede contener letras");
    }
    if (departamento == undefined) {
        throw new Error("Debes seleccionar un departamento")
    }
    if (ciudad == undefined) {
        throw new Error("Debes seleccionar una ciudad")
    }
    if (fechaNacimiento == "") {
        throw new Error("Debes ingresar una fecha")
    }
    let hoy = new Date();
    //toma el string de la fecha y lo convierte en un objeto Date para poder usar setHours
    let fechaNac = new Date(fechaNacimiento + 'T00:00:00');
    //se usa setHours(0, 0, 0, 0) para comparar solo fechas y no horas, minutos,etc
    fechaNac.setHours(0, 0, 0, 0);
    hoy.setHours(0, 0, 0, 0);
    if (fechaNac > hoy) {
        throw new Error("La fecha de nacimiento no puede ser mayor que la fecha actual")
    }
    if (ocupacion == undefined) {
        throw new Error("Debes seleccionar una ocupación")
    }
}


function ObtenerYCargarDepartamentos() {
    // if (connection) {
        fetch("https://censo.develotion.com/departamentos.php", {
            headers: {
                "Content-Type": "application/json",
                "apikey": localStorage.getItem("apikey"),
                "iduser": localStorage.getItem("iduser")
            }
        })
            .then(ManejarRespuesta)
            .then(function (datos) {
                CargarSelectDepartamentos(datos.departamentos);
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function CargarSelectDepartamentos(departamentos) {
    selectDepartamentos.value = undefined;
    let opcion = "";
    departamentos.forEach((departamento) => {
        opcion += `<ion-select-option value=${departamento.id}>${departamento.nombre}</ion-select-option>`;
    });
    document.querySelector("#slcDepartamentos").innerHTML = opcion;
}


function ObtenerYCargarCiudadesXDepartamento() {
    // if (connection) {
        idDepartamento = document.querySelector("#slcDepartamentos").value;
        fetch(
            `https://censo.develotion.com/ciudades.php?idDepartamento=${idDepartamento}`,
            {
                headers: {
                    "Content-Type": "application/json",
                    "apikey": localStorage.getItem("apikey"),
                    "iduser": localStorage.getItem("iduser")
                }
            })
            .then(ManejarRespuesta)
            .then(function (datos) {
                CargarSelectCiudades(datos.ciudades);
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function CargarSelectCiudades(ciudades) {
    //Se limpia el value de la ciudad para controlar el escenario en que el usuario selecciona un departamento,
    //luego selecciona una ciudad y después vuelva a cambiar el departamento.
    //De esta forma si cambia de departamento la ciudad no queda con el value seleccionado anteriormente por defecto(que correspondía al departamento anterior)
    selectCiudades.value = undefined;
    let opcion = "";
    ciudades.forEach((ciudad) => {
        opcion += `<ion-select-option value=${ciudad.id}>${ciudad.nombre}</ion-select-option>`
    });
    document.querySelector("#slcCiudades").innerHTML = opcion;
}



function ObtenerOcupaciones() {
    // if (connection) {
        return fetch("https://censo.develotion.com/ocupaciones.php", {
            headers: {
                "Content-Type": "application/json",
                "apikey": localStorage.getItem("apikey"),
                "iduser": localStorage.getItem("iduser")
            }
        })
            .then(ManejarRespuesta)
            .then(function (datos) {
                return datos.ocupaciones;
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


//Para evitar hacer dos funciones, una para cada select. Se le pasa por párametro el id del select que tiene que cargar
function CargarSelectOcupaciones(idSelect) {
    ObtenerOcupaciones()
        .then(function (ocupaciones) {
            let opcion = "";
            ocupaciones.forEach((ocupacion) => {
                opcion += `<ion-select-option value=${ocupacion.id}>${ocupacion.ocupacion}</ion-select-option>`;
            });
            document.querySelector("#" + idSelect).innerHTML = opcion;
        })
}


function CargarSelectOcupacionesXEdad() {
    selectOcupaciones.value = undefined;
    let fechaNacimiento = document.querySelector("#dateFechaNac").value;
    if (CalcularEdad(fechaNacimiento) < 18) {
        selectOcupaciones.innerHTML = `<ion-select-option value="5">Estudiante</ion-select-option>`;
    } else {
        CargarSelectOcupaciones("slcOcupaciones");
    }
}


function CalcularEdad(fechaNacimiento) {
    // Uso "+ 'T00:00:00'" para que al crearse el nuevo objeto Date lo haga a las 0 horas de la zona horaria local
    // Si usaba let fechaNac = new Date(fechaNacimiento) la creaba en UTC y generaba un error
    // Si ponía una fecha de nacimiento un día después de la fecha 
    // actual pero del año pasado devolvía 1 año de edad, lo cual está mal por que todavía no había pasado el cumpleaños
    let fechaNac = new Date(fechaNacimiento + 'T00:00:00');
    let fechaActual = new Date();
    // Poner a cero las horas para evitar discrepancias por la hora del día
    fechaActual.setHours(0, 0, 0, 0);
    // Calcular la diferencia en años
    let edad = fechaActual.getFullYear() - fechaNac.getFullYear();
    // Si la fecha actual es menor que la fecha de cumpleaños de este año,
    // quiere decir que todavía no pasó el cumpleaños este año y se le resta un año
    let fechaCumpleaniosAnioActual = new Date(
        fechaActual.getFullYear(),
        fechaNac.getMonth(),
        fechaNac.getDate()
    );
    if (fechaActual < fechaCumpleaniosAnioActual) {
        edad--;
    }
    return edad;
}


function ObtenerPersonas() {
    // if (connection) {
        let idUsuario = localStorage.getItem("iduser");
        return fetch(`https://censo.develotion.com/personas.php?idUsuario=${idUsuario}`, {
            headers: {
                "Content-Type": "application/json",
                "apikey": localStorage.getItem("apikey"),
                "iduser": localStorage.getItem("iduser")
            },
        })
            .then(ManejarRespuesta)
            .then(function (datos) {
                return datos.personas;
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}

//no lleva el if(connection) por que ya esta la validación en ObtenerPersonas y ObtenerOcupaciones
//tampoco lleva catch por que esta presente en ObtenerPersonas y ObtenerOcupaciones
function ListarTodasPersonas() {
    ObtenerOcupaciones()
        .then(function (ocupaciones) {
            return ObtenerPersonas()
                .then(function (personas) {
                    return { ocupaciones, personas }
                });
        })
        .then(function (datos) {
            CargarTablaPersonas(datos.personas, datos.ocupaciones);
        })
}


//no lleva el if(connection) por que ya esta la validación en ObtenerPersonas y ObtenerOcupaciones
//tampoco lleva catch por que esta presente en ObtenerPersonas y ObtenerOcupaciones
function ListarPersonasXOcupacion() {
    ObtenerOcupaciones()
        .then(function (ocupaciones) {
            return ObtenerPersonas()
                .then(function (personas) {
                    return { ocupaciones, personas }
                });
        })
        .then(function (datos) {
            let idOcupacion = selectFiltroOcupaciones.value;
            ValidarDatosOcupacion(idOcupacion);
            personasFiltradas = ObtenerPersonasXOcupacion(idOcupacion, datos.personas);
            CargarTablaPersonas(personasFiltradas, datos.ocupaciones);
        })
        .catch(function (error) {
            MostrarMensaje(error.message);
        });
}


function ValidarDatosOcupacion(idOcupacion) {
    if (idOcupacion == undefined) {
        throw new Error("Debes seleccionar una ocupación")
    }
}


function ObtenerPersonasXOcupacion(idOcupacion, personas) {
    return personas.filter((persona) => persona.ocupacion == idOcupacion);
}


function CargarTablaPersonas(personas, ocupaciones) {
    let datosTabla = "";
    personas.forEach((persona) => {
        let ocup = BuscarOcupacion(persona.ocupacion, ocupaciones);
        datosTabla += `<ion-row>
            <ion-col>${persona.nombre}</ion-col>
            <ion-col>${persona.fechaNacimiento}</ion-col>
            <ion-col>${ocup.ocupacion}</ion-col>
            <ion-col><ion-button color="light"  size="small"  onclick="EliminarPersona(${persona.id})"><ion-icon name="trash-outline"></ion-icon></ion-button></ion-col>
          </ion-row>`;
    });
    document.querySelector("#tablaInicioBody").innerHTML = datosTabla;
}


function BuscarOcupacion(idOcupacion, ocupaciones) {
    return ocupaciones.find((ocu) => ocu.id == idOcupacion);
}


function EliminarPersona(idPersona) {
    // if (connection) {
        fetch(`https://censo.develotion.com/personas.php?idCenso=${idPersona}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "apikey": localStorage.getItem("apikey"),
                "iduser": localStorage.getItem("iduser")
            },
        })
            .then(ManejarRespuesta)
            .then(function (datos) {
                if (datos.codigo == 200) {
                    ListarTodasPersonas();
                    MostrarMensaje(datos.mensaje);
                } else if (datos.codigo == 404) {
                    //si el usuario después de eliminar la persona, vuelve a apretar el botón de eliminar la misma persona antes de que se recargue de nuevo la tabla, la response de la API
                    //da status 200 pero en el body llega:  "codigo": 404, "mensaje": "Falta el id de censo"
                    //para manejar ese caso con un mensaje entendible para el usuario lanzo el siguiente error
                    throw new Error("Ya ha eliminado a esa persona, espere a que se recargue la tabla");
                }
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function ListarCantPersonasMvdInteriorYTotales() {
    ObtenerPersonas()
        .then(function (personas) {
            let resultado = ObtenerCantPersonasMvdInteriorYTotales(personas);
            CargarTablaCantPersonas(resultado);
        })
}


function ObtenerCantPersonasMvdInteriorYTotales(personas) {
    let acumulador = { total: 0, montevideo: 0, interior: 0 }
    personas.forEach(persona => {
        if (persona.departamento == 3218) {
            acumulador.montevideo++;
        } else if (persona.departamento !== 3218) {
            acumulador.interior++;
        }
        acumulador.total++;
    });
    return acumulador;
}


function CargarTablaCantPersonas(cantPersonas) {
    let datosTabla = `<ion-row>
    <ion-col>${cantPersonas.total}</ion-col>
    <ion-col>${cantPersonas.montevideo}</ion-col>
    <ion-col>${cantPersonas.interior}</ion-col>
  </ion-row>`;
    document.querySelector("#tablaCantPersonas").innerHTML = datosTabla;
}


function ObtenerCiudades() {
    // if (connection) {
        return fetch("https://censo.develotion.com/ciudades.php", {
            headers: {
                "Content-Type": "application/json",
                "apikey": localStorage.getItem("apikey"),
                "iduser": localStorage.getItem("iduser")
            }
        })
            .then(ManejarRespuesta)
            .then(function (datos) {
                return datos.ciudades;
            })
            .catch(ManejarError)
    // } else {
    //     MostrarMensaje("No estás conectado a internet. Por favor, revisa tu conexión.")
    // }
}


function ObtenerCiudadesDeCensados() {
    let ciudadesCensados = new Array;
    return ObtenerPersonas()
        .then(function (personas) {
            return ObtenerCiudades()
                .then(function (ciudades) {
                    return { ciudades, personas }
                });
        })
        .then(function (datos) {
            datos.personas.forEach(persona => {
                let ciudad = BuscarCiudad(persona.ciudad, datos.ciudades)
                if (!ExisteEnCiudadesCensados(ciudad.id, ciudadesCensados)) {
                    ciudadesCensados.push(ciudad);
                }
            });
            return ciudadesCensados;
        })
}


function BuscarCiudad(idCiudad, ciudades) {
    return ciudades.find((ciudad) => ciudad.id == idCiudad);
}


function ExisteEnCiudadesCensados(idCiudad, ciudadesCensados) {
    return ciudadesCensados.some((ciudad) => ciudad.id == idCiudad);
}


function ObtenerUbicacion() {
    navigator.geolocation.getCurrentPosition(SetearCoordenadasOrigen, MostrarError);
}


function SetearCoordenadasOrigen(position) {
    latitudeOrigen = position.coords.latitude;
    longitudeOrigen = position.coords.longitude;
    MostrarMapa();
}


function MostrarError() {
    if (map != null || map != undefined) {
        map.remove();
    }
    map = L.map('map').setView([-32.64, -55.11], 5);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap'
    }).addTo(map);
    MostrarMensaje("Error de geolocalización");
}


function MostrarMapa() {
    try {
        let distanciaKm = Number(document.querySelector("#numDistancia").value);
        ValidarDatosDistancia(distanciaKm);
        let distanciaMetros = distanciaKm * 1000;
        if (map != null || map != undefined) {
            map.remove();
        }
        map = L.map('map').setView([latitudeOrigen, longitudeOrigen], 14);
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '© OpenStreetMap'
        }).addTo(map);

        L.marker([latitudeOrigen, longitudeOrigen]).bindPopup("Mi ubicación").addTo(map);
        var circle = L.circle([latitudeOrigen, longitudeOrigen], {
            color: '0077cc',
            fillColor: '#0077cc',
            fillOpacity: 0.2,
            radius: distanciaMetros
        }).addTo(map);
        if (distanciaKm > 0) {
            map.fitBounds(circle.getBounds());
        }
        ObtenerCiudadesDeCensados().then(function (ciudadesCensados) {
            ciudadesCensados.forEach(ciudad => {
                let distancia = map.distance([latitudeOrigen, longitudeOrigen], [ciudad.latitud, ciudad.longitud]);
                if (distancia <= distanciaMetros) {
                    L.marker([ciudad.latitud, ciudad.longitud]).bindPopup(ciudad.nombre).addTo(map);
                }
            })
        })
    } catch (Error) {
        MostrarMensaje(Error.message);
    }

}


function ValidarDatosDistancia(distanciaKm) {
    if (distanciaKm < 0) {
        throw new Error("No es posible ingresar valores negativos")
    }
}







// npx cap sync
// npx cap run android 
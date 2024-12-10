use FoodInspections_mongo
db.createCollection("Conversaciones")

// Cada documento en esta colección representaría una conversación de un día específico entre todos los inspectores.
//(como si fuera un grupo de chat).
// En cada conversación se almacena las interacciones diarias en las cuales los inspectores deben compartir 
//la información de las inspecciones realizadas ese día.
// El chat por el cual se comunican los inspectores  tendría una funcionalidad que permite al inspector adjuntar los detalles de la inspección
//directamente en el mensaje.
// Este puede ser un documento adjunto o un enlace a un reporte más detallado.
// Los mensajes se componen de texto y dan la posibilidad de ajuntar la información de las inspecciones.
// Para acotar los datos ingresados y hacer mas fácil la corrección de las consultas se restringe la cantidad de inspectores a 3
//y la cantidad de conversaciones a 6.






conversacion1 = {
    "conversacionId": 1,
    "fechaConversacion": "2024-06-05",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector1",
            "hora": "09:30",
            "texto": "Realicé la inspección en Los Yuyos, lamentablemente hubo 2 violaciones.",
            "inspeccion": {
                "inspId": "1",
                "estNumero": "1",
                "estNombre": "Los Yuyos",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "3", "descripcion": "Almacenamiento inadecuado alimentos" },
                    { "codigo": "7", "descripcion": "Sin de extintores de incendio" }
                ]
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector2",
            "hora": "10:00",
            "texto": "Acabo de inspeccionar el Club de la Papa Frita, todo en orden excepto por algunos detalles menores.",
            "inspeccion": {
                "inspId": "2",
                "estNumero": "2",
                "estNombre": "Club de la Papa Frita",
                "inspResultado": "Pasa con condiciones",
                "violaciones": [
                    { "codigo": "4", "descripcion": "Uso indebido de químicos" }
                ]
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector3",
            "hora": "10:30",
            "texto": "Inspectores, en Bar Fraternidad no se encontró la oficina. Necesitamos verificar la dirección.",
            "inspeccion": {
                "inspId": "3",
                "estNumero": "12",
                "estNombre": "Bar Fraternidad",
                "inspResultado": "Oficina no encontrada",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg4",
            "de": "inspector3",
            "hora": "11:00",
            "texto": "Gracias por el aviso sobre Los Yuyos."
        }
    ]
}




conversacion2 = {
    "conversacionId": 2,
    "fechaConversacion": "2024-06-06",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector2",
            "hora": "09:45",
            "texto": "Inspeccioné Bar Los 4 y  el resultado es falla por múltiples violaciones.",
            "inspeccion": {
                "inspId": "4",
                "estNumero": "4",
                "estNombre": "Bar Los 4",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "5", "descripcion": "Personal sin exámenes médicos" },
                    { "codigo": "6", "descripcion": "Presencia de plagas o animales" }
                ]
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector3",
            "hora": "10:15",
            "texto": "Acabo de volver de Tasende, todo en perfecto estado. Aprobado sin condiciones.",
            "inspeccion": {
                "inspId": "5",
                "estNumero": "5",
                "estNombre": "Tasende",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector1",
            "hora": "11:00",
            "texto": "Parrillada Modelo no pasó la inspección.",
            "inspeccion": {
                "inspId": "6",
                "estNumero": "6",
                "estNombre": "Parrillada Modelo",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "2", "descripcion": "Higiene deficiente" },
                    
                ]
            }
        },
        {
            "mensajeId": "msg4",
            "de": "inspector2",
            "hora": "11:30",
            "texto": "Buen trabajo con Tasende. Vamos a necesitar un plan de acción para la Parrillada Modelo."
        }
    ]
}



conversacion3 = {
    "conversacionId": 3,
    "fechaConversacion": "2024-06-07",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector3",
            "hora": "09:30",
            "texto": "El Mesón Español no pasó la inspección hoy debido a varias violaciones.",
            "inspeccion": {
                "inspId": "7",
                "estNumero": "9",
                "estNombre": "El Mesón Español",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "3", "descripcion": "Almac. inadecuado alimentos" },
                    { "codigo": "5", "descripcion": "Personal sin exámenes médicos" }
                ]
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector1",
            "hora": "10:00",
            "texto": "Hoy en Pizzería Rodelu, se pasó con condiciones. Algunos problemas menores a corregir.",
            "inspeccion": {
                "inspId": "8",
                "estNumero": "8",
                "estNombre": "Pizzería Rodelu",
                "inspResultado": "Pasa con condiciones",
                "violaciones": [
                    { "codigo": "4", "descripcion": "Uso indebido de químicos" }
                ]
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector2",
            "hora": "11:00",
            "texto": "Inspección en El Fogón concluida. Todo correcto y sin violaciones.",
            "inspeccion": {
                "inspId": "9",
                "estNumero": "10",
                "estNombre": "El Fogón",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        }
    ]    
}    

conversacion4 = {
    "conversacionId": 4,
    "fechaConversacion": "2024-06-08",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector1",
            "hora": "09:40",
            "texto": "Inspección en McDonald’s completada con éxito, todo en norma.",
            "inspeccion": {
                "inspId": "10",
                "estNumero": "7",
                "estNombre": "McDonald’s",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector3",
            "hora": "10:20",
            "texto": "Revisé Bar Sporting y pasó con condiciones, pero hay áreas de mejora.",
            "inspeccion": {
                "inspId": "11",
                "estNumero": "3",
                "estNombre": "Bar Sporting",
                "inspResultado": "Pasa con condiciones",
                "violaciones": [
                    { "codigo": "8", "descripcion": "Ventilación insuficiente" }
                ]
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector2",
            "hora": "11:10",
            "texto": "Parrillada Modelo aún no cumple con los estándares. Falla grave.",
            "inspeccion": {
                "inspId": "12",
                "estNumero": "6",
                "estNombre": "Parrillada Modelo",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "2", "descripcion": "Higiene deficiente" },
                    { "codigo": "7", "descripcion": "Sin de extintores de incendio" }
                ]
            }
        },
        {
            "mensajeId": "msg4",
            "de": "inspector2",
            "hora": "11:45",
            "texto": "Necesitamos implementar mejoras en Bar Sporting antes de la próxima inspección."
        }
    ]
}


conversacion5 = {
    "conversacionId": 5,
    "fechaConversacion": "2024-06-09",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector3",
            "hora": "09:50",
            "texto": "Tasende pasó sin problemas, todo perfecto como la última vez.",
            "inspeccion": {
                "inspId": "13",
                "estNumero": "5",
                "estNombre": "Tasende",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector2",
            "hora": "10:30",
            "texto": "Club de la Papa Frita no ha mostrado una mejora desde la última inspección.",
            "inspeccion": {
                "inspId": "14",
                "estNumero": "2",
                "estNombre": "Club de la Papa Frita",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "4", "descripcion": "Uso indebido de químicos" }
                ]
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector1",
            "hora": "11:15",
            "texto": "Problemas en Bar Los 4 continúan, falla en la inspección.",
            "inspeccion": {
                "inspId": "15",
                "estNumero": "4",
                "estNombre": "Bar Los 4",
                "inspResultado": "Falla",
                "violaciones": [
                    { "codigo": "5", "descripcion": "Personal sin exámenes médicos" },
                    { "codigo": "6", "descripcion": "Presencia de plagas o animales" }
                ]
            }
        },
        {
            "mensajeId": "msg4",
            "de": "inspector3",
            "hora": "11:40",
            "texto": "De acuerdo, vamos a planificar un seguimiento intensivo para Bar Los 4."
        }
    ]
}



conversacion6 = {
    "conversacionId": 6,
    "fechaConversacion": "2024-06-10",
    "mensajes": [
        {
            "mensajeId": "msg1",
            "de": "inspector2",
            "hora": "11:45",
            "texto": "Segunda inspección del mes en McDonald’s completada con éxito.",
            "inspeccion": {
                "inspId": "16",
                "estNumero": "7",
                "estNombre": "McDonald’s",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg2",
            "de": "inspector3",
            "hora": "12:15",
            "texto": "Ultima inspección de Tasende, todo de nuevo en perfecto estado.",
            "inspeccion": {
                "inspId": "17",
                "estNumero": "5",
                "estNombre": "Tasende",
                "inspResultado": "Pasa",
                "violaciones": []
            }
        },
        {
            "mensajeId": "msg3",
            "de": "inspector1",
            "hora": "14:40",
            "texto": "Bien, esos estableciminetos siempre cumplen, Hoy no me corresponde realizar ninguna isnpección."
        }
    ]
}

db.Conversaciones.insertMany([conversacion1, conversacion2, conversacion3, conversacion4, conversacion5, conversacion6])


//a)Cuantas conversaciones sobre violaciones diferentes se constataron.

// Se interpreta la consulta como la cantidad de conversaciones que tuvo cada violacion diferente
// Ejemplo: si el codigo de violacion 1 estuvo prensente en la conversacion1 y conversacion2, entonces la consulta devolvería 
//numeroConversaciones:2, codigoViolacion:1
//La consulta devuelve el resultado ordenado por numeroConversaciones descendente.

db.Conversaciones.aggregate([
    {
        $unwind: "$mensajes"
    },
    {
        $unwind: "$mensajes.inspeccion.violaciones"
    },
    {
        $group: {
            _id: {
                codigo: "$mensajes.inspeccion.violaciones.codigo",
                conversacionId: "$conversacionId"
            }
        }
    },
    {
        $group: {
            _id: "$_id.codigo",
            numeroConversaciones: { $sum: 1 }
        }
    },
    {
        $project: {
            _id: 0,
            codigoViolacion: "$_id",
            numeroConversaciones: 1
        }
    },
    { $sort: { numeroConversaciones: -1 } }
])


//b)Obtener los mejores establecimientos basado en la cantidad de inspecciones aprobadas.

// Se muestran los mejores 2 establecimientos ordenados por cantidad de inspecciones con resultado 'Pasa' descendente.
// Solo se consideran aprobadas las inspecciones con resultado 'Pasa'.


db.Conversaciones.aggregate([
    { $unwind: "$mensajes" },
    { $match: { "mensajes.inspeccion.inspResultado": "Pasa" } },
    {
        $group: {
            _id: "$mensajes.inspeccion.estNombre",
            count: { $sum: 1 }
        }
    },
    { $sort: { count: -1 } },
    { $limit: 2 },
    {
        $project: {
            _id: 0,  
            establecimiento: "$_id",  
            cantidadInspeccionesAprobadas: "$count" 
        }
    }
])


//c)Modificar una conversación agregando una etiqueta “IMPORTANTE” para todos aquellos chats que tengan referencia
//a resultados reprobados ('Falla').

// Se interpretó  agregar una etiqueta como clave (IMPORTANTE) y se decidió darle valor true a esa clave 
// El código se hizo para  modificar una conversación y no todas, por eso se usa updateOne y se pasa el
//argumento ({ "conversacionId": 1 }) que es el filtro que determina cuál documento se debe actualizar.

db.Conversaciones.updateOne(
    { "conversacionId": 1 }, 
    { 
        $set: {
            "mensajes.$[elem].IMPORTANTE": true
        }
    }, 
    { 
        arrayFilters: [{ "elem.inspeccion.inspResultado": "Falla" }]
    }
);




db.Conversaciones.find({"conversacionId":1 }) 
   

// CONSULTAS EXTRA


// Obtener todos los nombres(sin repetir) de los establecimientos inspeccionados.

db.Conversaciones.distinct("mensajes.inspeccion.estNombre")



// Eliminar todos los mensajes sin inspección en una conversación específica(conversacion6 en el ejemplo).
// (Solo borra los mensajes que tenían solo texto de una coversación)

db.Conversaciones.updateOne(
    { conversacionId: 6 }, 
    { $pull: { mensajes: { inspeccion: { $exists: false } } } }
)

db.Conversaciones.find( { conversacionId: 6 })


// Crear un índice en el campo texto.

db.Conversaciones.createIndex({ "mensajes.texto": "text" })


// Devolver los índices que existen en la colección Conversaciones.

db.Conversaciones.getIndexes()


// Buscar las conversaciones que contengan la palabra "violaciones" en alguno de sus mensajes utilizando el índice creado.

db.Conversaciones.find({ $text: { $search: "violaciones" } })

// Crear una colección con documentos de todas las inspecciones con resultado 'Falla'

db.Conversaciones.aggregate([
    { $unwind: "$mensajes" },
    { $match: { "mensajes.inspeccion.inspResultado": "Falla" } },
    { $project: { 
        _id: 0 
        inspeccion: "$mensajes.inspeccion",
    }},
    { $out: "reportesInspeccionesFalla" }
]);



db.reportesInspeccionesFalla.find()


//  Obtener todas las inspecciones realizadas por un inspector específico(inspector3 en el ejemplo).

db.Conversaciones.aggregate([
    { $unwind: "$mensajes" },
    { $match: { "mensajes.de": "inspector3", "mensajes.inspeccion": { $exists: true } } },
    { $project: { 
        _id: 0,
        "inspId": "$mensajes.inspeccion.inspId",
        "estNumero": "$mensajes.inspeccion.estNumero",
        "estNombre": "$mensajes.inspeccion.estNombre",
        "inspResultado": "$mensajes.inspeccion.inspResultado",
        "violaciones": "$mensajes.inspeccion.violaciones"
    }}
])


// Esta consulta devuelve todas las conversaciones que contienen alguna inspección en las que se registraron 
// violaciones con todos los códigos especificados(3 y 7 en el ejemlplo).

db.Conversaciones.find({
    "mensajes.inspeccion.violaciones.codigo": {
        $all: ["3", "7"]
    }
})



//  Obtener las inspecciones en las que se registró al menos uno de los códigos de violación especificados(ejemplo 3 o 7).

db.Conversaciones.aggregate([
    { $unwind: "$mensajes" },
    {
        $match: {
            "mensajes.inspeccion.violaciones.codigo": {
                $in: ["3", "7"] 
            }
        }
    },
    {
        $project: {
            _id: 0,
            inspeccion: "$mensajes.inspeccion"
        }
    }
]);


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.RepoInterfaces;
using BusinessLogic.Entities;
using System.Text.Json;

namespace DataAccessLogic.Api.Utilidades
{
    public class ManejoRespuestas
    {
        public ManejoRespuestas()
        {
        }

        public T HandleResponse<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return DeserializeContent<T>(response.Content);

                case HttpStatusCode.NotFound:
                    throw new Exception("Elemento no encontrado.");

                case HttpStatusCode.BadRequest:
                    var error = DeserializeContent<ApiException>(response.Content);
                    throw new Exception(error?.Message ?? "Error en la solicitud.");

                case HttpStatusCode.NoContent:
                    return default(T);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new Exception("Acceso no autorizado.");

                case HttpStatusCode.InternalServerError:
                    throw new Exception("Hubo un problema con el servidor.");

                default:
                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeContent<T>(response.Content);
                    }
                    else
                    {
                        throw new Exception("Hubo un problema con un servicio externo.");
                    }
            }
        }

        private static T DeserializeContent<T>(HttpContent content)
        {
            if (content == null || content.Headers.ContentLength == 0)
            {
                return default(T);
            }

            try
            {
                return content.ReadFromJsonAsync<T>().Result;
            }
            catch (JsonException)
            {
                throw new Exception("Error al deserializar el contenido de la respuesta.");
            }
        }
    }
}

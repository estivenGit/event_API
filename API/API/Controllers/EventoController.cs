using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Transversal.Models;
using LogicaNegocio.Services.Evento;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using System.Web;

namespace API.Controllers
{
    /// <summary>
    /// Controlador que expone las operaciones de gestión de eventos.
    /// </summary>
    [Route("api/Evento")]
    public class EventoController : ApiController
    {
        private readonly IEventoService _eventoService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="eventoService">Servicio para gestionar eventos.</param>
        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        /// <summary>
        /// Obtiene todos los eventos.
        /// </summary>
        /// <returns>Lista de eventos.</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAllEvents()
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                var eventos = await _eventoService.GetAllEventsAsync(codigoUsuario);
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al obtener los eventos: {ex.Message}"));
            }
        }



        /// <summary>
        /// Obtiene un evento por su ID.
        /// </summary>
        /// <param name="id">ID del evento.</param>
        /// <returns>Evento encontrado o un estado 404 si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IHttpActionResult> GetEventById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventByIdAsync(id);
                if (evento == null)
                {
                    return Content(System.Net.HttpStatusCode.NotFound,$"Evento con ID {id} no encontrado.");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al obtener el evento: {ex.Message}"));
            }
        }

        /// <summary>
        /// Crea un nuevo evento.
        /// </summary>
        /// <param name="evento">Modelo del evento a crear.</param>
        /// <returns>Resultado de la creación del evento.</returns>
        [HttpPost]
        public async Task<IHttpActionResult> CreateEvent([FromBody] EventoModel evento)
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                evento.IdUsuarioCreador = codigoUsuario;
                await _eventoService.CreateEventAsync(evento);
                var uri = Url.Link("DefaultApi", new { controller = "evento", id = evento.IdEvento });

                // Devolver el evento con el código de estado 201 (Created)
                return Created(uri, evento);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($" Error al crear el evento: {ex.Message}"));
            }
        }

        /// <summary>
        /// Actualiza un evento existente.
        /// </summary>
        /// <param name="id">ID del evento a actualizar.</param>
        /// <param name="evento">Modelo con los nuevos detalles del evento.</param>
        /// <returns>Resultado de la operación de actualización.</returns>
        [HttpPut]
        public async Task<IHttpActionResult> put(int id, [FromBody] EventoModel evento)
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                evento.IdUsuarioCreador = codigoUsuario;
                evento.IdEvento = id;
                await _eventoService.UpdateEventAsync(evento);
                return Ok($"Evento actualizado correctamente.");

            }
            catch (UnauthorizedAccessException)
            {
                return Content(System.Net.HttpStatusCode.Conflict, "No tiene permisos para actualizar este evento.");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($" Error al actualizar el evento: {ex.Message}"));
            }
        }
       

        /// <summary>
        /// Elimina un evento por su ID.
        /// </summary>
        /// <param name="id">ID del evento a eliminar.</param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        [HttpDelete("{id}")]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                EventoModel evento = new EventoModel
                {
                    IdEvento = id,
                    IdUsuarioCreador = codigoUsuario
                };
                await _eventoService.DeleteEventAsync(evento);

                return Ok($"Evento con eliminado correctamente.");

            }
            catch (UnauthorizedAccessException ex)
            {
                return Content(System.Net.HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($" Error al eliminar el evento: {ex.Message}"));
            }
        }
    }
}

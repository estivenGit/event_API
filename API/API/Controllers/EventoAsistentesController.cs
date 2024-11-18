using LogicaNegocio.Services.AsistenciaEvento;
using LogicaNegocio.Services.Evento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Transversal.Models;
using Newtonsoft.Json;
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
    /// Controlador para gestionar las asistencias a eventos.
    /// </summary>
    [Route("api/EventoAsistentes")]
    public class EventoAsistentesController : ApiController
    {
        private readonly IEventoAsistentesService _eventoAsistentesService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="eventoAsistentesService">Servicio para gestionar asistencias a eventos.</param>
        public EventoAsistentesController(IEventoAsistentesService eventoAsistentesService)
        {
            _eventoAsistentesService = eventoAsistentesService;
        }

        /// <summary>
        /// Crea una nueva asistencia a un evento.
        /// </summary>
        /// <param name="evento">Modelo que contiene los datos de la asistencia.</param>
        /// <returns>Resultado de la creación de la asistencia, incluyendo el código 201 si es exitoso.</returns>
        [HttpPost("{id}")]
        public async Task<IHttpActionResult> CreateEventAsistent(int id)
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                EventoAsistenteModel evento = new EventoAsistenteModel
                {
                    IdEvento = id,
                    IdUsuario = codigoUsuario
                };

                await _eventoAsistentesService.CreateAsync(evento);
                var uri = Url.Link("DefaultApi", new { controller = "evento", id = evento.IdEvento });

                return Created(uri, evento);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de errores al crear una asistencia.
                return InternalServerError(new Exception($"Error al crear el asistente del evento: {ex.Message}"));
            }
        }

        /// <summary>
        /// Elimina una asistencia a un evento.
        /// </summary>
        /// <param name="id">ID del evento al que se asocia la asistencia a eliminar.</param>
        /// <param name="evento">Modelo que contiene los datos del evento y el asistente a eliminar.</param>
        /// <returns>Resultado de la operación, incluyendo un mensaje de confirmación si es exitoso.</returns>
        [HttpDelete("{id}")]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            try
            {
                var codigoUsuario = int.Parse(HttpContext.Current.Items["codigo_usuario"]?.ToString());
                EventoAsistenteModel evento = new EventoAsistenteModel
                {
                    IdEvento = id,
                    IdUsuario = codigoUsuario
                };

                await _eventoAsistentesService.DeleteEventAsync(evento);

                return Ok($"Asistencia al evento eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error al eliminar el asistente del evento: {ex.Message}"));
            }
        }
    }
}

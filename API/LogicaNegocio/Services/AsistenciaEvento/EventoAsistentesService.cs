using AccesoDatos.db;
using AccesoDatos.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;
using Transversal.Utilidades;

namespace LogicaNegocio.Services.AsistenciaEvento
{
    /// <summary>
    /// Servicio para gestionar la lógica de negocio de asistencias a eventos.
    /// </summary>
    public class EventoAsistentesService : IEventoAsistentesService
    {
        private readonly IEventoAsistentesRepository _eventoAsistentesRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EventoAsistentesService"/>.
        /// </summary>
        /// <param name="eventoAsistentesRepository">Repositorio para acceder a datos de asistencias a eventos.</param>
        /// <param name="loggerService">Servicio para registrar errores y eventos.</param>
        /// <param name="eventoRepository">Repositorio para acceder a datos de eventos.</param>
        public EventoAsistentesService(IEventoAsistentesRepository eventoAsistentesRepository, ILoggerService loggerService, IEventoRepository eventoRepository)
        {
            _eventoAsistentesRepository = eventoAsistentesRepository;
            _loggerService = loggerService;
            _eventoRepository = eventoRepository;
        }

        /// <summary>
        /// Crea una nueva asistencia a un evento.
        /// </summary>
        /// <param name="asistenciaEvento">Modelo que contiene los datos de la asistencia.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task CreateAsync(EventoAsistenteModel asistenciaEvento)
        {
            try
            {
                var eventoEntity = new Evento_Asistentes
                {
                    Id_Evento = asistenciaEvento.IdEvento,
                    Id_Usuario = asistenciaEvento.IdUsuario
                };

                bool validaciones = await validaCrearAsistencia(eventoEntity);
                if (validaciones)
                    await _eventoAsistentesRepository.CreateAsync(eventoEntity);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error al crear la asistencia.");
                throw;
            }
        }

        /// <summary>
        /// Elimina una asistencia a un evento.
        /// </summary>
        /// <param name="evento">Modelo que contiene los datos de la asistencia a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task DeleteEventAsync(EventoAsistenteModel evento)
        {
            try
            {
                var eventAsistencia = await _eventoAsistentesRepository.GetUsuarioEventoAsistencias(evento.IdUsuario, evento.IdEvento);
                await _eventoAsistentesRepository.DeleteAsync(eventAsistencia.Id_EventoAsistente);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error. No se encontró la asistencia de evento con id {evento.IdEventoAsistente}.");
                throw;
            }
        }

        /// <summary>
        /// Devuelve cantidad de asistencias por Id Evento.
        /// </summary>
        public async Task<int> CantidadAsistentesByEvent(int IdEvent)
        {
            try
            {
                var cantidadAsistentesByEvent = await _eventoAsistentesRepository.GetByFieldAsync("Id_Evento", IdEvent);
                return cantidadAsistentesByEvent.Count();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error CantidadAsistentesByEvent.");
                throw;
            }
        }

        /// <summary>
        /// Devuelve cantidad de asistencias por Id Evento.
        /// </summary>
        public async Task<int> CantidadAsistentesByUser(int IdUser)
        {
            try
            {
                var cantidadAsistentesByEvent = await _eventoAsistentesRepository.GetByFieldAsync("Id_Usuario", IdUser);
                return cantidadAsistentesByEvent.Count();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error CantidadAsistentesByUser.");
                throw;
            }
        }

        /// <summary>
        /// Devuelve cantidad de asistencias por Id Evento.
        /// </summary>
        public async Task<bool> usuarioEventoAsistencia(int idUser, int idEvent)
        {
            try
            {
                var asistentes = await _eventoAsistentesRepository.GetUsuarioEventoAsistencias(idUser, idEvent);

                return asistentes!=null;
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error CantidadAsistentesByUser.");
                throw;
            }
        }

        /// <summary>
        /// Valida si una asistencia puede ser creada según las reglas del negocio.
        /// </summary>
        /// <param name="asistenciaEventoEntity">Entidad que representa la asistencia.</param>
        /// <returns>Un valor booleano que indica si la asistencia cumple las reglas de negocio.</returns>
        private async Task<bool> validaCrearAsistencia(Evento_Asistentes asistenciaEventoEntity)
        {
            try
            {
                var eventoCreado = await _eventoRepository.GetByIdAsync((int)asistenciaEventoEntity.Id_Evento);

                // Regla: Los usuarios no pueden inscribirse en eventos que hayan creado.
                if (eventoCreado.Id_UsuarioCreador == asistenciaEventoEntity.Id_Usuario)
                    throw new UnauthorizedAccessException("No se puede asociar a un evento que usted creó.");

                var cantidadEventosByUser = await CantidadAsistentesByUser((int)asistenciaEventoEntity.Id_Usuario);

                // Regla: Cada usuario puede inscribirse en un máximo de 3 eventos diferentes.
                if (cantidadEventosByUser >= 3)
                    throw new UnauthorizedAccessException("No se puede inscribir a más de 3 eventos.");

                var cantidadAsistentesByEvent = await CantidadAsistentesByEvent((int)asistenciaEventoEntity.Id_Evento);

                // Regla: No se puede exceder la capacidad máxima de asistentes.
                if (eventoCreado.Capacidad <= cantidadAsistentesByEvent)
                    throw new UnauthorizedAccessException("Ya se ha alcanzado la capacidad máxima de asistentes permitida.");

                var usuarioInscrito = await usuarioEventoAsistencia((int)asistenciaEventoEntity.Id_Usuario, (int)asistenciaEventoEntity.Id_Evento);
                // Regla: No se puede exceder la capacidad máxima de asistentes.
                if (usuarioInscrito)
                    throw new UnauthorizedAccessException("Ya se encuentra inscrito al evento.");

                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error al validar reglas de asistencia.");
                return false;
            }
        }
    }
}

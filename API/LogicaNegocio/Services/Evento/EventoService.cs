using AccesoDatos.db;
using AccesoDatos.Repositories;
using LogicaNegocio.Services.AsistenciaEvento;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;
using Transversal.Utilidades;

namespace LogicaNegocio.Services.Evento
{
    /// <summary>
    /// Servicio que proporciona operaciones para gestionar eventos, como crear, actualizar, eliminar y consultar eventos.
    /// </summary>
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ILoggerService _loggerService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEventoAsistentesService _eventoAsistentesService;

        /// <summary>
        /// Constructor de la clase EventoService.
        /// </summary>
        /// <param name="eventoRepository">Repositorio para acceder a los datos de los eventos.</param>
        /// <param name="loggerService">Servicio para manejar logs de errores y eventos.</param>
        public EventoService(IEventoRepository eventoRepository, ILoggerService loggerService, IUsuarioRepository usuarioRepository, IEventoAsistentesService eventoAsistentesService)
        {
            _eventoRepository = eventoRepository;
            _loggerService = loggerService;
            _usuarioRepository = usuarioRepository;
            _eventoAsistentesService = eventoAsistentesService;
        }

        /// <summary>
        /// Crea un nuevo evento.
        /// </summary>
        /// <param name="evento">Modelo de evento a crear.</param>
        /// <returns>Una tarea asincrónica que completa la operación de creación del evento.</returns>
        public async Task CreateEventAsync(EventoModel evento)
        {
            try
            {
                var eventoEntity = new Eventos
                {
                    Nombre = evento.Nombre,
                    Descripcion = evento.Descripcion,
                    FechaHora = evento.FechaHora,
                    Capacidad = evento.Capacidad,
                    Ubicacion = evento.Ubicacion,
                    Activo = true,
                    Id_UsuarioCreador = evento.IdUsuarioCreador,
                };

                await _eventoRepository.CreateAsync(eventoEntity);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error al crear el evento: {evento.Nombre}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza los detalles de un evento existente.
        /// </summary>
        /// <param name="evento">Modelo con los detalles del evento a actualizar.</param>
        /// <returns>Una tarea asincrónica que completa la operación de actualización del evento.</returns>
        public async Task UpdateEventAsync(EventoModel evento)
        {
            try
            {
                if (!await validaPermisoActualizar(evento))
                    throw new UnauthorizedAccessException("No tiene permisos para actualizar este evento.");

                var eventoEntity = new Eventos
                {
                    Id_Evento = evento.IdEvento,
                    Capacidad = evento.Capacidad,
                    FechaHora = evento.FechaHora,
                    Ubicacion = evento.Ubicacion
                };

                await _eventoRepository.UpdateAsync(eventoEntity);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error al actualizar el evento: {evento.Nombre}");
                throw;
            }
        }

        /// <summary>
        /// Elimina un evento por su ID.
        /// </summary>
        /// <param name="id">ID del evento a eliminar.</param>
        /// <returns>Una tarea asincrónica que completa la operación de eliminación del evento.</returns>
        public async Task DeleteEventAsync(EventoModel evento)
        {
            try
            {
                if (!await validaPermisoActualizar(evento))
                    throw new UnauthorizedAccessException("No tiene permisos para eliminar este evento.");

                //valida si tiene asistentes inscritos
                var cantidadAsistentesByEvent = await _eventoAsistentesService.CantidadAsistentesByEvent(evento.IdEvento);
                if (cantidadAsistentesByEvent > 0)
                    throw new UnauthorizedAccessException("No se puede eliminar el evento, tiene asistentes inscritos");

                await _eventoRepository.DeleteAsync(evento.IdEvento);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error al eliminar el evento con ID {evento.IdEvento}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un evento por su ID.
        /// </summary>
        /// <param name="id">ID del evento a obtener.</param>
        /// <returns>Un modelo de evento con los detalles del evento, o null si no se encuentra.</returns>
        public async Task<EventoModel> GetEventByIdAsync(int id)
        {
            try
            {
                var eventoEntity = await _eventoRepository.GetByIdAsync(id);
                if (eventoEntity == null)
                    return null;

                return new EventoModel
                {
                    IdEvento = eventoEntity.Id_Evento,
                    Nombre = eventoEntity.Nombre,
                    FechaHora = (DateTime)eventoEntity.FechaHora,
                    Descripcion = eventoEntity.Descripcion,
                    Capacidad = (int)eventoEntity.Capacidad,
                    Ubicacion = eventoEntity.Ubicacion,
                    Activo = (bool)eventoEntity.Activo,
                    IdUsuarioCreador = (int)eventoEntity.Id_UsuarioCreador,
                    userAsistente = await _eventoAsistentesService.usuarioEventoAsistencia(
                                                                    (int)eventoEntity.Id_UsuarioCreador,
                                                                    eventoEntity.Id_Evento)
                };
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error al obtener el evento con ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los eventos registrados.
        /// </summary>
        /// <returns>Una lista de modelos de eventos.</returns>
        public async Task<IEnumerable<EventoModel>> GetAllEventsAsync(int currentUser)
        {
            try
            {
                var eventosEntities = await _eventoRepository.GetAllAsync();
                var eventoModels = new List<EventoModel>();
                foreach (var entity in eventosEntities)
                {
                    var eventoModel = new EventoModel
                    {
                        IdEvento = entity.Id_Evento,
                        Nombre = entity.Nombre,
                        FechaHora = (DateTime)entity.FechaHora,
                        Descripcion = entity.Descripcion,
                        Capacidad = (int)entity.Capacidad,
                        Ubicacion = entity.Ubicacion,
                        Activo = (bool)entity.Activo,
                        IdUsuarioCreador = (int)entity.Id_UsuarioCreador,
                        esCreador= (int)entity.Id_UsuarioCreador== currentUser,
                        userAsistente = await _eventoAsistentesService.usuarioEventoAsistencia(
                                            currentUser,
                                            entity.Id_Evento),
                        CantidadAsistentes=await _eventoAsistentesService.CantidadAsistentesByEvent(entity.Id_Evento)
                    };

                    eventoModels.Add(eventoModel);
                }

                return eventoModels;
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error al obtener todos los eventos.");
                throw;
            }
        }

        /// <summary>
        /// Valida si el usuario tiene permisos para actualizar un evento.
        /// </summary>
        /// <param name="evento">El modelo del evento que se desea actualizar.</param>
        /// <returns>Devuelve true si el usuario tiene permisos para actualizar el evento, de lo contrario, false.</returns>
        private async Task<bool> validaPermisoActualizar(EventoModel evento)
        {
            var eventoCreado = await GetEventByIdAsync(evento.IdEvento);

            if (eventoCreado == null)
                new InvalidOperationException("No existe el elemento");


            return eventoCreado.IdUsuarioCreador == evento.IdUsuarioCreador;
        }

    }
}

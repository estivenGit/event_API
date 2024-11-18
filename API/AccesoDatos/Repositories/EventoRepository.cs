using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Utilidades;

namespace AccesoDatos.Repositories
{
    /// <summary>
    /// Repositorio para la entidad <see cref="Eventos"/>. 
    /// Proporciona operaciones asincrónicas para la creación, eliminación, 
    /// actualización, y consulta de eventos en la base de datos.
    /// </summary>
    public class EventoRepository : IEventoRepository
    {
        private readonly DC_APIContainer _context;
        private readonly ILoggerService _loggerService;
        private bool _disposed = false;

        /// <summary>
        /// Constructor de <see cref="EventoRepository"/>.
        /// </summary>
        /// <param name="context">El contexto de base de datos.</param>
        /// <param name="loggerService">Servicio para registrar errores.</param>
        public EventoRepository(DC_APIContainer context, ILoggerService loggerService)
        {
            _context = context;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Crea un nuevo evento de manera asincrónica.
        /// </summary>
        /// <param name="entity">Entidad de tipo <see cref="Eventos"/> a crear.</param>
        /// <exception cref="ArgumentNullException">Se lanza si la entidad es null.</exception>
        public async Task CreateAsync(Eventos entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Eventos.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error creando el evento: {entity.Nombre}");
                throw;
            }
        }

        /// <summary>
        /// Elimina un evento por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">ID del evento a eliminar.</param>
        /// <exception cref="KeyNotFoundException">Se lanza si no se encuentra el evento con el ID proporcionado.</exception>
        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Eventos.FindAsync(id);

                if (entity == null)
                    throw new KeyNotFoundException($"No se encontró el evento con ID {id}");

                _context.Eventos.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error eliminando el evento con ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los eventos de manera asincrónica.
        /// </summary>
        /// <returns>Una lista de todos los eventos.</returns>
        public async Task<IEnumerable<Eventos>> GetAllAsync()
        {
            try
            {
                return await _context.Eventos.ToListAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error consultando todos los eventos");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un evento por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">ID del evento a obtener.</param>
        /// <returns>El evento con el ID especificado.</returns>
        public async Task<Eventos> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Eventos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error consultando evento por Id: {id}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un evento de manera asincrónica.
        /// </summary>
        /// <param name="entity">Entidad de tipo <see cref="Eventos"/> a actualizar.</param>
        /// <exception cref="ArgumentNullException">Se lanza si la entidad es null.</exception>
        public async Task UpdateAsync(Eventos entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var existingEntity = _context.Eventos.Local.FirstOrDefault(e => e.Id_Evento == entity.Id_Evento);

                if (existingEntity == null)
                {
                    throw new InvalidOperationException("La entidad no fue encontrada.");
                }

                existingEntity.Capacidad = entity.Capacidad;
                existingEntity.FechaHora = entity.FechaHora;
                existingEntity.Ubicacion = entity.Ubicacion;

                // No es necesario llamar a SetValues si solo estamos actualizando campos específicos
                _context.Entry(existingEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error actualizando evento: {entity.Nombre}");
                throw;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por el repositorio.
        /// </summary>
        /// <param name="disposing">Indica si los recursos gestionados por el objeto deben ser liberados.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por el repositorio.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

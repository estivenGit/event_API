using AccesoDatos.db;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Transversal.Utilidades;

namespace AccesoDatos.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de entidades <see cref="Evento_Asistentes"/>.
    /// Proporciona métodos para crear, consultar y manipular datos relacionados con asistencias a eventos.
    /// </summary>
    public class EventoAsistentesRepository : IEventoAsistentesRepository, IDisposable
    {
        private readonly DC_APIContainer _context;
        private readonly ILoggerService _loggerService;
        private bool _disposed = false;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EventoAsistentesRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de la base de datos utilizado para acceder a los datos.</param>
        /// <param name="loggerService">Servicio de registro de errores y eventos.</param>
        public EventoAsistentesRepository(DC_APIContainer context, ILoggerService loggerService)
        {
            _context = context;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Crea una nueva asistencia de evento en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad <see cref="Evento_Asistentes"/> a agregar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// <exception cref="ArgumentNullException">Si la entidad proporcionada es nula.</exception>
        public async Task CreateAsync(Evento_Asistentes entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Evento_Asistentes.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error creando la asistencia de eventos");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una lista de asistencias de eventos filtradas por un campo específico.
        /// </summary>
        /// <param name="fieldName">Nombre del campo por el que se filtrará.</param>
        /// <param name="value">Valor que debe coincidir en el campo especificado.</param>
        /// <returns>Una tarea que representa la operación asincrónica con una lista de asistencias que coinciden con el filtro.</returns>
        /// <exception cref="ArgumentException">Si el campo especificado no existe en el modelo.</exception>
        public async Task<IEnumerable<Evento_Asistentes>> GetByFieldAsync(string fieldName, object value)
        {
            try
            {
                var property = typeof(Evento_Asistentes).GetProperty(fieldName);
                if (property == null)
                    throw new ArgumentException($"El campo '{fieldName}' no existe en el modelo Evento_Asistentes.");

                var parameter = Expression.Parameter(typeof(Evento_Asistentes), "x");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);

                // Manejo de propiedades nullable
                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    var hasValue = Expression.Property(propertyAccess, "HasValue");
                    var valueExpression = Expression.Property(propertyAccess, "Value");
                    var constantValue = Expression.Constant(value);

                    var condition = Expression.AndAlso(
                        Expression.Equal(hasValue, Expression.Constant(true)),
                        Expression.Equal(valueExpression, constantValue));

                    var lambda = Expression.Lambda<Func<Evento_Asistentes, bool>>(condition, parameter);
                    return await _context.Evento_Asistentes.Where(lambda).ToListAsync();
                }

                var comparison = Expression.Equal(propertyAccess, Expression.Constant(value));
                var filter = Expression.Lambda<Func<Evento_Asistentes, bool>>(comparison, parameter);

                return await _context.Evento_Asistentes.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error consultando asistencia por campo '{fieldName}' con valor '{value}'");
                throw;
            }
        }

        /// <summary>
        /// Obtiene la asistencia de un usuario a un evento específico.
        /// </summary>
        /// <param name="idUser">ID del usuario.</param>
        /// <param name="idEvent">ID del evento.</param>
        /// <returns>Una tarea con la asistencia del usuario al evento.</returns>
        public async Task<Evento_Asistentes> GetUsuarioEventoAsistencias(int idUser, int idEvent)
        {
            try
            {
                return await _context.Evento_Asistentes
                    .FirstOrDefaultAsync(x => x.Id_Evento == idEvent && x.Id_Usuario == idUser);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error consultando asistencias con filtros");
                throw;
            }
        }

        /// <summary>
        /// Elimina una asistencia de evento por su ID.
        /// </summary>
        /// <param name="id">ID de la asistencia a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Evento_Asistentes.FindAsync(id);

                if (entity == null)
                    throw new KeyNotFoundException($"No se encontró la asistencia de evento con ID {id}");

                _context.Evento_Asistentes.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error eliminando la asistencia de evento con ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por el repositorio.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        public Task UpdateAsync(Evento_Asistentes entity)
        {
            throw new NotImplementedException();
        }

        public Task<Evento_Asistentes> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Evento_Asistentes>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

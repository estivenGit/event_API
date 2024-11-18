using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;
using Transversal.Utilidades;
using Z.EntityFramework.Plus;

namespace AccesoDatos.Repositories
{
    /// <summary>
    /// Repositorio para gestionar las operaciones relacionadas con los tokens de refresco en la base de datos.
    /// </summary>
    public class TokenRefreshRepository : ITokenRefreshRepository
    {
        private readonly DC_APIContainer _context;
        private readonly DbSet<Usuarios_TokenRefresh> _dbSet;
        private readonly IPasswordManage _passwordManage;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Constructor de la clase <see cref="TokenRefreshRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos utilizado para las operaciones.</param>
        /// <param name="passwordManage">Servicio para la gestión de contraseñas.</param>
        /// <param name="loggerService">Servicio para la gestión de logs.</param>
        public TokenRefreshRepository(DC_APIContainer context, IPasswordManage passwordManage, ILoggerService loggerService)
        {
            _context = context;
            _dbSet = context.Set<Usuarios_TokenRefresh>();
            _passwordManage = passwordManage;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Crea un nuevo registro de token de refresco en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad del token de refresco que será agregada.</param>
        public async Task CreateAsync(Usuarios_TokenRefresh entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error guardando token de refresco para el usuario con ID: " + entity.Id_Usuario);
                throw;
            }
        }

        /// <summary>
        /// Actualiza el estado del token de refresco para un usuario y registra un nuevo token.
        /// </summary>
        /// <param name="user">Usuario al que pertenece el token.</param>
        /// <param name="newToken">Nuevo token de refresco a registrar.</param>
        public async Task updateTokenRefreshByUserAsync(Usuarios user, string newToken)
        {
            try
            {
                // Desactivar tokens existentes para el usuario.
                await _context.Usuarios_TokenRefresh
                    .Where(u => u.Id_Usuario == user.Id_Usuario && u.EstaActivo == true)
                    .UpdateAsync(u => new Usuarios_TokenRefresh
                    {
                        EstaActivo = false,
                        FechaRevocacion = DateTime.Now,
                        TokenReemplazo = newToken
                    });
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error actualizando tokens de refresco para el usuario con ID: {user.Id_Usuario}");
                throw;
            }
        }

        /// <summary>
        /// Valida si un token de refresco es válido y está activo para un usuario específico.
        /// </summary>
        /// <param name="userID">Identificador único del usuario.</param>
        /// <param name="tokenRefresh">Token de refresco a validar.</param>
        /// <returns>
        /// <c>true</c> si el token es válido y está activo; de lo contrario, <c>false</c>.
        /// </returns>
        public async Task<bool> validarRefreshTokenAsync(int userID, string tokenRefresh)
        {
            try
            {
                return await _context.Usuarios_TokenRefresh
                    .AnyAsync(u => u.Id_Usuario == userID &&
                                   u.Token == tokenRefresh &&
                                   u.EstaActivo == true);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error validando token de refresco para el usuario con ID: {userID}");
                throw;
            }
        }
    }
}
